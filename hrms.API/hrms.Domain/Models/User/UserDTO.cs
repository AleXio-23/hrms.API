namespace hrms.Domain.Models.User
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;


        public string Email { get; set; } = null!;


        public bool? IsActive { get; set; }

        public UserProfileDTO? UserProfileDTO { get; set; }
    }
}
