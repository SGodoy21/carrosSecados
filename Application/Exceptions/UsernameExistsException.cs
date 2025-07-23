namespace Application.Exceptions
{
    public class UsernameExistsException : System.Exception
    {
        public UsernameExistsException(string username) : base($"El nombre de usuario '{username}' ya está registrado.") { }
    }
}
