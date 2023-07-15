namespace hrms.Domain.Models.User
{
    public class UserDTO
    {
        public int? Id { get; set; }

        public string Username { get; set; } = null!;


        public string Email { get; set; } = null!;


        public bool? IsActive { get; set; }


        public string? OldPassword{ get; set; }
        public string? NewPassword{ get; set; }
        public string? NewPasswordConfirmation{ get; set; }

        public UserProfileDTO? UserProfileDTO { get; set; }
    }
}
