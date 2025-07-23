namespace Application.Exceptions
{
    public class InvalidEmailException : System.Exception
    {
        public InvalidEmailException(string email) : base($"El email '{email}' es inválido.") { }
    }
}
