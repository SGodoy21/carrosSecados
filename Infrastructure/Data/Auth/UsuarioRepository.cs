using Application.Interfaces.Auth;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data.Auth
{
    public class UsuarioRepository(axLoginCleanContext context) : IUsuarioRepository
    {
        private readonly axLoginCleanContext _context = context;

        public async Task<Usuario> GetByIdAsync(long usuarioId)
        {
            return await _context.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.Id == usuarioId);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> GetByAliasAsync(string alias)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == alias);
        }

        public async Task<Usuario> GetByAliasAndPasswordAsync(string alias, string password)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == alias && u.PasswordHash == password);
        }

        public async Task<Usuario> GetByUsernameAsync(string username)
        {
            return await _context.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.NombreUsuario == username);
        }

        public async Task<IEnumerable<Usuario>> GetByDynamicFilterAsync(JsonElement filtro)
        {
            var filters = ToDictionary(filtro);

            IQueryable<Usuario> q = _context.Usuarios.AsNoTracking();

            // ✅ whitelist: propiedades públicas reales de la entidad
            // Podés restringir aún más si querés
            var allowedProps = typeof(Usuario)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            foreach (var (key, rawValue) in filters)
            {
                if (rawValue is null) continue;

                // Solo si existe en la entidad
                if (!allowedProps.TryGetValue(key, out var prop))
                    continue;

                q = ApplyEqualsFilter(q, prop, rawValue);
            }

            var users = await q.ToListAsync();

            return users.ToList();
        }

        private static Dictionary<string, object?> ToDictionary(JsonElement element)
        {
            var dict = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);

            foreach (var p in element.EnumerateObject())
            {
                dict[p.Name] = p.Value.ValueKind switch
                {
                    JsonValueKind.String => p.Value.GetString(),
                    JsonValueKind.Number => p.Value.TryGetInt64(out var l) ? l :
                                           p.Value.TryGetDecimal(out var d) ? d : (object?)p.Value.GetDouble(),
                    JsonValueKind.True => true,
                    JsonValueKind.False => false,
                    JsonValueKind.Null => null,
                    _ => p.Value.ToString() // arrays/objetos: si los necesitás, lo extendemos
                };
            }

            return dict;
        }

        private static IQueryable<Usuario> ApplyEqualsFilter(IQueryable<Usuario> query, PropertyInfo prop, object rawValue)
        {
            var param = Expression.Parameter(typeof(Usuario), "x");
            var member = Expression.Property(param, prop);

            // manejar Nullable<T>
            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

            // string => Contains (case-insensitive opcional)
            if (targetType == typeof(string))
            {
                var str = rawValue.ToString()?.Trim();
                if (string.IsNullOrEmpty(str)) return query;

                // x.Prop != null && x.Prop.Contains(str)
                var notNull = Expression.NotEqual(member, Expression.Constant(null, prop.PropertyType));
                var contains = Expression.Call(
                    member,
                    typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) })!,
                    Expression.Constant(str)
                );

                var body = Expression.AndAlso(notNull, contains);
                var lambda = Expression.Lambda<Func<Usuario, bool>>(body, param);
                return query.Where(lambda);
            }

            // convertir rawValue al tipo real
            object converted;
            try
            {
                converted = Convert.ChangeType(rawValue, targetType);
            }
            catch
            {
                return query; // si no se puede convertir, lo ignoramos
            }

            // x.Prop == converted (cuidando nullables)
            Expression constant = Expression.Constant(converted, targetType);

            if (prop.PropertyType != targetType) // nullable
                constant = Expression.Convert(constant, prop.PropertyType);

            var equal = Expression.Equal(member, constant);
            var lambdaEq = Expression.Lambda<Func<Usuario, bool>>(equal, param);
            return query.Where(lambdaEq);
        }

        public async Task<bool> AddAsync(Usuario newUser)
        {
            _context.Usuarios.Add(newUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateAsync(Usuario user)
        {
            _context.Usuarios.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Usuario>> GetUsersByRoleIdAsync(int roleId)
        {
            return await _context.Usuarios.Where(u => u.RolId == roleId).ToListAsync();
        }

        public async Task DeleteAsync(Usuario user)
        {
            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}