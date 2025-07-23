namespace Shared.DTOs
{
    public class ChangeUserRoleDto
    {
        public long UserId { get; set; }
        public int RoleId { get; set; }
        // Opcional: public string ChangedBy { get; set; }
    }
}
