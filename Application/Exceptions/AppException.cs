using System;

namespace Application.Exceptions
{
    public class AppException : Exception
    {
        private AppException(string message) : base(message)
        {
        }

        public static AppException InvalidEmail(string email)
            => new AppException($"El email '{email}' es inválido.");

        public static AppException RoleInUse(long id)
            => new AppException($"No se puede eliminar el rol con id {id} porque está asignado a usuarios.");

        public static AppException RoleNameExists(string name)
            => new AppException($"El nombre de rol '{name}' ya existe.");

        public static AppException RoleNotFound(long id)
            => new AppException($"Rol con id {id} no encontrado.");

        public static AppException UsernameExists(string username)
            => new AppException($"El nombre de usuario '{username}' ya está registrado.");

        public static AppException UserNotFound(long id)
            => new AppException($"Usuario con id {id} no encontrado.");

        public static AppException InvalidPassword()
            => new AppException("La contraseña debe tener al menos 8 caracteres.");

        public static AppException ProductAlreadyExists(decimal codigo)
    => new AppException($"El producto con código {codigo} ya existe.");

        public static AppException ProductNotFound(decimal codigo)
            => new AppException($"El producto con código {codigo} no fue encontrado.");
    }
}