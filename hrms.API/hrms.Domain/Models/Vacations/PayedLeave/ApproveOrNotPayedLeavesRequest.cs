namespace hrms.Domain.Models.Vacations.PayedLeave
{
    public class ApproveOrNotPayedLeavesRequest
    {
        public int UserId { get; set; }
        public int PayedLeaveId { get; set; }
        public bool IsApproved { get; set; } 
        public string? Comment { get; set; } = null;
    }
}
