namespace hrms.Domain.Models.Dictionary.Departments
{
    public class DepartmentDTO
    {
        public int? Id { get; set; } 
        public string Name { get; set; } = null!; 
        public bool? IsActive { get; set; }
    }
}
