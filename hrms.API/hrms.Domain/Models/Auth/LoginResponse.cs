namespace hrms.Domain.Models.Auth
{
    public class LoginResponse
    {
        public int? Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AccessToken { get; set; }
        public LoginResponsePositions? LoginResponsePositions { get; set; }
    }
}
