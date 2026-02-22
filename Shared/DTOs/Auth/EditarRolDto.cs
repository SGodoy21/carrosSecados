namespace Shared.DTOs.Auth
{
    public class EditarRolDto
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
    }

    public class RolResponseDto
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
    }

    public class CrearRolDto
    {
        public string Nombre { get; set; }
    }
}