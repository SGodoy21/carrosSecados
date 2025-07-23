namespace Application.Exceptions
{
    public class UserNotFoundException : System.Exception
    {
        public UserNotFoundException(int id) : base($"Usuario con id {id} no encontrado.") { }
    }
}
