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
    public class RoleService
    {
        private static readonly string[] ProtectedRoles = new[] { "Admin", "SuperAdmin" };
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public RoleService(IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task<List<RoleResponseDto>> GetAllAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            return roles.Select(r => new RoleResponseDto { Id = r.Id, Name = r.Name }).ToList();
        }

        public async Task<RoleResponseDto> CreateAsync(RoleCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Length < 3 || dto.Name.Length > 100)
                throw new ArgumentException("El nombre debe tener entre 3 y 100 caracteres.");
            if (!Regex.IsMatch(dto.Name, "^[a-zA-Z0-9_]+$"))
                throw new ArgumentException("El nombre solo puede contener letras, números y guiones bajos.");
            if (ProtectedRoles.Contains(dto.Name, StringComparer.OrdinalIgnoreCase))
                throw new InvalidOperationException("No se puede crear un rol protegido.");

            var exists = (await _roleRepository.GetAllAsync())
                .FirstOrDefault(r => r.Name.Equals(dto.Name, System.StringComparison.OrdinalIgnoreCase));
            if (exists != null)
                throw new RoleNameExistsException(dto.Name);

            var role = new Role { Name = dto.Name };
            await _roleRepository.AddAsync(role);

            return new RoleResponseDto { Id = role.Id, Name = role.Name };
        }

        public async Task<RoleResponseDto> EditAsync(RoleEditDto dto)
        {
            var role = await _roleRepository.GetByIdAsync(dto.Id);
            if (role == null)
                throw new RoleNotFoundException(dto.Id);
            if (ProtectedRoles.Contains(role.Name, StringComparer.OrdinalIgnoreCase))
                throw new InvalidOperationException("No se puede modificar un rol protegido.");
            if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Length < 3 || dto.Name.Length > 100)
                throw new ArgumentException("El nombre debe tener entre 3 y 100 caracteres.");
            if (!Regex.IsMatch(dto.Name, "^[a-zA-Z0-9_]+$"))
                throw new ArgumentException("El nombre solo puede contener letras, números y guiones bajos.");
            if (ProtectedRoles.Contains(dto.Name, StringComparer.OrdinalIgnoreCase))
                throw new InvalidOperationException("No se puede asignar un nombre de rol protegido.");
            var exists = (await _roleRepository.GetAllAsync())
                .FirstOrDefault(r => r.Name.Equals(dto.Name, System.StringComparison.OrdinalIgnoreCase));
            if (exists != null && exists.Id != dto.Id)
                throw new RoleNameExistsException(dto.Name);

            role.Name = dto.Name;
            await _roleRepository.UpdateAsync(role);

            return new RoleResponseDto { Id = role.Id, Name = role.Name };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
                throw new RoleNotFoundException(id);
            if (ProtectedRoles.Contains(role.Name, StringComparer.OrdinalIgnoreCase))
                throw new InvalidOperationException("No se puede eliminar un rol protegido.");
            var usersWithRole = await _userRepository.GetUsersByRoleIdAsync(id);
            if (usersWithRole.Any())
                throw new RoleInUseException(id);

            await _roleRepository.DeleteAsync(role);
            return true;
        }
    }
}

