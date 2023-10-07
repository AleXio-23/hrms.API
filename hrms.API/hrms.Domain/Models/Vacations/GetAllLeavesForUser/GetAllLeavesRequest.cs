namespace hrms.Domain.Models.Vacations.GetAllLeavesForUser
{
    public class GetAllLeavesRequest
    {
        public int UserId { get; set; }
        public List<string> LeaveTypes { get; set; } = new List<string>();
        public int? Offset { get; set; }
        public int? Limit { get; set; }
    }
}
