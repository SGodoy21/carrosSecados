namespace Shared.DTOs
{
    public class EditarRolDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class RolResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class CrearRolDto
    {
        public string Nombre { get; set; }
    }
}