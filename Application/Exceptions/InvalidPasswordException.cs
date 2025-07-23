namespace Application.Exceptions
{
    public class InvalidPasswordException : System.Exception
    {
        public InvalidPasswordException() : base("La contraseña debe tener al menos 8 caracteres.") { }
    }
}
