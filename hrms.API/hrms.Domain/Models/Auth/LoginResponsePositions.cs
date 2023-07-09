using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hrms.Domain.Models.Auth
{
    public class LoginResponsePositions
    {
        public int? PositionId { get; set; }
        public string? Position { get; set; }
        public int? DepartmentId { get; set; }
        public string? Department { get; set; }
    }
}
