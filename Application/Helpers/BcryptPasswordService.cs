using Application.Interfaces;
using Application.Interfaces.Auth;
using BCrypt.Net;

namespace Application.Helpers
{
    public class BcryptPasswordService : IPasswordService
    {
        public string Encriptar(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verificar(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
