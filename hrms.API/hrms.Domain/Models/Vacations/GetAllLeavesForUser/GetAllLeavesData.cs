namespace hrms.Domain.Models.Vacations.GetAllLeavesForUser
{
    public class GetAllLeavesData
    {
        public int LeaveId { get; set; }
        public string LeaveType { get; set; }
        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public int? CountDays { get; set; }
        public bool? Approved { get; set; } = false;

        public string? ApprovedByFullname { get; set; }
    }
}
