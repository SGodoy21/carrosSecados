using Application.Interfaces.Auth;

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