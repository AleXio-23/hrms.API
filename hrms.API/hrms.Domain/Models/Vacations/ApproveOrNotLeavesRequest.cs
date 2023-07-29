namespace hrms.Domain.Models.Vacations
{
    public class ApproveOrNotLeavesRequest
    { 
        public int LeaveId { get; set; }
        public bool IsApproved { get; set; }
        public string? Comment { get; set; } = null;
    }
}
