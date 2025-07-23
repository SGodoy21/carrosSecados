using Application.Helpers;
using Application.Interfaces;
using Domain.Entities;
using System.Threading.Tasks;

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
                throw new System.Exception("Usuario no encontrado.");

            var role = await _roleRepository.GetByIdAsync(dto.RoleId);
            if (role == null)
                throw new System.Exception("Rol no encontrado.");

            user.RoleId = dto.RoleId;
            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> RegistrarUsuarioAsync(Shared.DTOs.RegisterUserDto dto)
        {
            // Validaciones mínimas
            if (string.IsNullOrWhiteSpace(dto.Password) || dto.Password.Length < 8)
                throw new System.Exception("La contraseña debe tener al menos 8 caracteres.");
            if (string.IsNullOrWhiteSpace(dto.Email) || !dto.Email.Contains("@"))
                throw new System.Exception("Email inválido.");

            // Verificar unicidad
            if (await _userRepository.GetByUsernameAsync(dto.Username) != null)
                throw new System.Exception("El nombre de usuario ya está registrado.");
            // Si tienes método para email, úsalo:
            // if (await _userRepository.GetByEmailAsync(dto.Email) != null)
            //     throw new System.Exception("El email ya está registrado.");

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
    }
}
