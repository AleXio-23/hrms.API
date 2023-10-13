namespace hrms.Domain.Models.User.Employees
{
    public class EmployeesListResponse
    {
        public int? UserId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Department { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Role { get; set; }
    }
}
