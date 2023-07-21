using System.ComponentModel.DataAnnotations;

namespace hrms.Domain.Models.Auth
{
    public class LoginDto
    {
        [Required]
        public string? EmailOrUsername { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
