namespace Application.Exceptions
{
    public class RoleNotFoundException : System.Exception
    {
        public RoleNotFoundException(int id) : base($"Rol con id {id} no encontrado.") { }
    }
}
