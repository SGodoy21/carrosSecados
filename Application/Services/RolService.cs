using Application.Interfaces;
using Domain.Entities;
using Shared.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Text.RegularExpressions;
using Application.Exceptions;

namespace Application.Services
{
    public class RolService
    {
        private static readonly string[] ProtectedRoles = new[] { "Admin", "SuperAdmin" };
        private readonly IRolRepository _roleRepository;
        private readonly IUsuarioRepository _userRepository;

        public RolService(IRolRepository roleRepository, IUsuarioRepository userRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task<List<RolResponseDto>> GetAllAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            return roles.Select(r => new RolResponseDto { Id = r.Id, Nombre = r.Nombre }).ToList();
        }

        public async Task<RolResponseDto> CreateAsync(CrearRolDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre) || dto.Nombre.Length < 3 || dto.Nombre.Length > 100)
                throw new ArgumentException("El nombre debe tener entre 3 y 100 caracteres.");
            if (!Regex.IsMatch(dto.Nombre, "^[a-zA-Z0-9_]+$"))
                throw new ArgumentException("El nombre solo puede contener letras, números y guiones bajos.");
            if (ProtectedRoles.Contains(dto.Nombre, StringComparer.OrdinalIgnoreCase))
                throw new InvalidOperationException("No se puede crear un rol protegido.");

            var exists = (await _roleRepository.GetAllAsync())
                .FirstOrDefault(r => r.Nombre.Equals(dto.Nombre, System.StringComparison.OrdinalIgnoreCase));
            if (exists != null)
                throw AppException.RoleNameExists(dto.Nombre);

            var role = new Rol { Nombre = dto.Nombre };
            await _roleRepository.AddAsync(role);

            return new RolResponseDto { Id = role.Id, Nombre = role.Nombre };
        }

        public async Task<RolResponseDto> EditAsync(EditarRolDto dto)
        {
            var role = await _roleRepository.GetByIdAsync(dto.Id);
            if (role == null)
                throw AppException.RoleNotFound(dto.Id);
            if (ProtectedRoles.Contains(role.Nombre, StringComparer.OrdinalIgnoreCase))
                throw new InvalidOperationException("No se puede modificar un rol protegido.");
            if (string.IsNullOrWhiteSpace(dto.Nombre) || dto.Nombre.Length < 3 || dto.Nombre.Length > 100)
                throw new ArgumentException("El nombre debe tener entre 3 y 100 caracteres.");
            if (!Regex.IsMatch(dto.Nombre, "^[a-zA-Z0-9_]+$"))
                throw new ArgumentException("El nombre solo puede contener letras, números y guiones bajos.");
            if (ProtectedRoles.Contains(dto.Nombre, StringComparer.OrdinalIgnoreCase))
                throw new InvalidOperationException("No se puede asignar un nombre de rol protegido.");
            var exists = (await _roleRepository.GetAllAsync())
                .FirstOrDefault(r => r.Nombre.Equals(dto.Nombre, System.StringComparison.OrdinalIgnoreCase));
            if (exists != null && exists.Id != dto.Id)
                throw AppException.RoleNameExists(dto.Nombre);

            role.Nombre = dto.Nombre;
            await _roleRepository.UpdateAsync(role);

            return new RolResponseDto { Id = role.Id, Nombre = role.Nombre };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
                throw AppException.RoleNotFound(id);
            if (ProtectedRoles.Contains(role.Nombre, StringComparer.OrdinalIgnoreCase))
                throw new InvalidOperationException("No se puede eliminar un rol protegido.");
            var usersWithRole = await _userRepository.GetUsersByRoleIdAsync(id);
            if (usersWithRole.Any())
                throw AppException.RoleInUse(id);

            await _roleRepository.DeleteAsync(role);
            return true;
        }
    }
}

