using Application.Helpers;
using Application.Interfaces;
using Domain.Entities;
using System.Threading.Tasks;
using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordService _passwordService;
        private readonly IRolRepository _rolRepository;

        public UsuarioService(IUsuarioRepository userRepository, IPasswordService passwordService, IRolRepository roleRepository)
        {
            _usuarioRepository = userRepository;
            _passwordService = passwordService;
            _rolRepository = roleRepository;
        }

        public async Task<bool> ChangeUsuarioRolAsync(Shared.DTOs.CambiarRolUsuarioDto dto)
        {
            var user = await _usuarioRepository.GetByIdAsync(dto.UsuarioId);
            if (user == null)
                throw AppException.UserNotFound((int)dto.UsuarioId);

            var role = await _rolRepository.GetByIdAsync(dto.RolId);
            if (role == null)
                throw AppException.RoleNotFound(dto.RolId);

            user.RolId = dto.RolId;
            await _usuarioRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> RegistrarUsuarioAsync(Shared.DTOs.RegistrarUsuarioDto dto)
        {
            // Validaciones mínimas
            if (string.IsNullOrWhiteSpace(dto.Password) || dto.Password.Length < 8)
                throw AppException.InvalidPassword();
            if (string.IsNullOrWhiteSpace(dto.Email) || !dto.Email.Contains("@"))
                throw AppException.InvalidEmail(dto.Email);

            // Verificar unicidad
            if (await _usuarioRepository.GetByUsernameAsync(dto.NombreUsuario) != null)
                throw AppException.UsernameExists(dto.NombreUsuario);
            // Si tienes método para email, úsalo:
            // if (await _usuarioRepository.GetByEmailAsync(dto.Email) != null)
            //     throw new InvalidEmailException(dto.Email);

            // Hashear contraseña
            var passwordHash = _passwordService.Encriptar(dto.Password);

            // Mapear manualmente DTO a entidad
            var user = new Domain.Entities.Usuario
            {
                NombreUsuario = dto.NombreUsuario,
                PasswordHash = passwordHash,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Telefono = dto.Telefono,
                Email = dto.Email,
                Habilitado = true,
                AccesosIncorrectos = 0,
                RecoveryToken = null,
                FechaExpiracion = null,
                FechaCreacion = System.DateTime.UtcNow,
                RolId = 2 // Rol por defecto: Usuario
            };

            await _usuarioRepository.AddAsync(user);
            return true;
        }

        public async Task<bool> ValidarLogin(string username, string password)
        {
            var user = await _usuarioRepository.GetByUsernameAsync(username);
            if (user == null) return false;
            return _passwordService.Verificar(password, user.PasswordHash);
        }

        public async Task<bool> DisableUsuarioAsync(long userId)
        {
            var user = await _usuarioRepository.GetByIdAsync(userId);
            if (user == null)
                throw AppException.UserNotFound((int)userId);
            if (!user.Habilitado)
                throw new InvalidOperationException("El usuario ya está deshabilitado.");

            user.Habilitado = false;
            await _usuarioRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> DeleteUsuarioAsync(long userId)
        {
            var user = await _usuarioRepository.GetByIdAsync(userId);
            if (user == null)
                throw AppException.UserNotFound((int)userId);

            await _usuarioRepository.DeleteAsync(user);
            return true;
        }

        public async Task<bool> EnableUsuarioAsync(long userId)
        {
            var user = await _usuarioRepository.GetByIdAsync(userId);
            if (user == null)
                throw AppException.UserNotFound((int)userId);
            if (user.Habilitado)
                throw new InvalidOperationException("El usuario ya está habilitado.");

            user.Habilitado = true;
            await _usuarioRepository.UpdateAsync(user);
            return true;
        }

        public async Task<List<Shared.DTOs.UsuarioDto>> GetAllUsuarioAsync()
        {
            var users = await _usuarioRepository.GetAllAsync();
            return users.Select(u => new Shared.DTOs.UsuarioDto
            {
                Id = u.Id,
                NombreUsuario = u.NombreUsuario,
                NombreApellido = $"{u.Nombre} {u.Apellido}",
                Email = u.Email,
                RolId = u.RolId,
                GrupoId = u.GrupoId,
                Habilitado = u.Habilitado
            }).ToList();
        }

        public async Task<bool> ChangePasswordAsync(long userId, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 8)
                throw AppException.InvalidPassword();
            var user = await _usuarioRepository.GetByIdAsync(userId);
            if (user == null)
                throw AppException.UserNotFound((int)userId);
            user.PasswordHash = _passwordService.Encriptar(newPassword);
            await _usuarioRepository.UpdateAsync(user);
            return true;
        }
    }
}
