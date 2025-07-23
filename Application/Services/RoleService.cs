using Application.Interfaces;
using Domain.Entities;
using Shared.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RoleService
    {
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
                throw new System.Exception("El nombre debe tener entre 3 y 100 caracteres.");
            if (!dto.Name.All(char.IsLetterOrDigit))
                throw new System.Exception("El nombre solo puede contener letras y números.");

            var exists = await _roleRepository.GetByNameAsync(dto.Name);
            if (exists != null)
                throw new System.Exception("Ya existe un rol con ese nombre.");

            var role = new Role { Name = dto.Name };
            await _roleRepository.AddAsync(role);

            return new RoleResponseDto { Id = role.Id, Name = role.Name };
        }

        public async Task<RoleResponseDto> EditAsync(RoleEditDto dto)
        {
            var role = await _roleRepository.GetByIdAsync(dto.Id);
            if (role == null)
                throw new System.Exception("Rol no encontrado.");

            var exists = await _roleRepository.GetByNameAsync(dto.Name);
            if (exists != null && exists.Id != dto.Id)
                throw new System.Exception("Ya existe un rol con ese nombre.");

            role.Name = dto.Name;
            await _roleRepository.UpdateAsync(role);

            return new RoleResponseDto { Id = role.Id, Name = role.Name };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
                throw new System.Exception("Rol no encontrado.");

            var usersWithRole = await _userRepository.GetUsersByRoleIdAsync(id);
            if (usersWithRole.Any())
                throw new System.Exception("No se puede eliminar un rol asignado a usuarios.");

            await _roleRepository.DeleteAsync(role);
            return true;
        }
    }
}

