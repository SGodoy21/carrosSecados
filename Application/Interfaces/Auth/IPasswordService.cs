namespace Application.Interfaces.Auth
{
    public interface IPasswordService
    {
        string Encriptar(string password);

        bool Verificar(string password, string hash);
    }
}