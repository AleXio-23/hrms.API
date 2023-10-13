namespace hrms.Domain.Models.User.Employees
{
    public class EmployeesFilter
    {
        public string? Fullname { get; set; }
        public List<int>? Departments { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public List<int>? Countries { get; set; }
        public List<int>? States { get; set; }
        public List<int>? Cities { get; set; }
        public List<int>? Roles { get; set; }
        public int? Offset { get; set; }
        public int? Limit { get; set; }
    }
}
