namespace Application.Exceptions
{
    public class RoleInUseException : System.Exception
    {
        public RoleInUseException(int id) : base($"No se puede eliminar el rol con id {id} porque está asignado a usuarios.") { }
    }
}
