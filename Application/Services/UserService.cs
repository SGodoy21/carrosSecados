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
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IPasswordService passwordService, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _roleRepository = roleRepository;
        }

        public async Task<bool> ChangeUserRoleAsync(Shared.DTOs.ChangeUserRoleDto dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user == null)
                throw new UserNotFoundException((int)dto.UserId);

            var role = await _roleRepository.GetByIdAsync(dto.RoleId);
            if (role == null)
                throw new RoleNotFoundException(dto.RoleId);

            user.RoleId = dto.RoleId;
            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> RegistrarUsuarioAsync(Shared.DTOs.RegisterUserDto dto)
        {
            // Validaciones mínimas
            if (string.IsNullOrWhiteSpace(dto.Password) || dto.Password.Length < 8)
                throw new InvalidPasswordException();
            if (string.IsNullOrWhiteSpace(dto.Email) || !dto.Email.Contains("@"))
                throw new InvalidEmailException(dto.Email);

            // Verificar unicidad
            if (await _userRepository.GetByUsernameAsync(dto.Username) != null)
                throw new UsernameExistsException(dto.Username);
            // Si tienes método para email, úsalo:
            // if (await _userRepository.GetByEmailAsync(dto.Email) != null)
            //     throw new InvalidEmailException(dto.Email);

            // Hashear contraseña
            var passwordHash = _passwordService.Hash(dto.Password);

            // Mapear manualmente DTO a entidad
            var user = new Domain.Entities.User
            {
                Username = dto.Username,
                PasswordHash = passwordHash,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Phone = dto.Phone,
                Email = dto.Email,
                IsEnabled = true,
                FailedAccessCount = 0,
                RecoveryToken = null,
                TokenExpiresAt = null,
                CreatedAt = System.DateTime.UtcNow,
                RoleId = 2 // Rol por defecto: User
            };

            await _userRepository.AddAsync(user);
            return true;
        }

        public async Task<bool> ValidarLogin(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null) return false;
            return _passwordService.Verify(password, user.PasswordHash);
        }

        public async Task<bool> DisableUserAsync(long userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new UserNotFoundException((int)userId);
            if (!user.IsEnabled)
                throw new InvalidOperationException("El usuario ya está deshabilitado.");

            user.IsEnabled = false;
            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> DeleteUserAsync(long userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new UserNotFoundException((int)userId);

            await _userRepository.DeleteAsync(user);
            return true;
        }

        public async Task<bool> EnableUserAsync(long userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new UserNotFoundException((int)userId);
            if (user.IsEnabled)
                throw new InvalidOperationException("El usuario ya está habilitado.");

            user.IsEnabled = true;
            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<List<Shared.DTOs.UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new Shared.DTOs.UserDto
            {
                Id = u.Id,
                Username = u.Username,
                FullName = $"{u.FirstName} {u.LastName}",
                Email = u.Email,
                RoleId = u.RoleId,
                GroupId = u.GroupId,
                IsEnabled = u.IsEnabled
            }).ToList();
        }

        public async Task<bool> ChangePasswordAsync(long userId, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 8)
                throw new InvalidPasswordException();
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new UserNotFoundException((int)userId);
            user.PasswordHash = _passwordService.Hash(newPassword);
            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}
