namespace Application.Exceptions
{
    public class RoleNameExistsException : System.Exception
    {
        public RoleNameExistsException(string name) : base($"El nombre de rol '{name}' ya existe.") { }
    }
}
