using hrms.Domain.Models.Dictionary.Departments;
using hrms.Domain.Models.Dictionary.JobPositions;

namespace hrms.Domain.Models.User
{
    public class UserJobPositionDTO
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }

        public int? PositionId { get; set; }

        public int? DepartmentId { get; set; }
        public virtual DepartmentDTO? Department { get; set; }

        public virtual JobPositionDTO? Position { get; set; }

    }
}
