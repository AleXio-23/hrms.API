using System.ComponentModel.DataAnnotations;

namespace hrms.Domain.Models.Auth
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 6)]
        public string RepeatPassword { get; set; }
    }
}
