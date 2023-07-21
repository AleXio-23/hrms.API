using System.ComponentModel.DataAnnotations;

namespace hrms.Domain.Models.Auth
{
    public class RegisterDto
    {
         
        [Required]
        public string? Email { get; set; }

        [Required]
        [StringLength(244, MinimumLength = 8)]
        public string? Password { get; set; }

        [Required]
        [StringLength(244, MinimumLength = 8)]
        public string? RepeatPassword { get; set; }
    }
}
