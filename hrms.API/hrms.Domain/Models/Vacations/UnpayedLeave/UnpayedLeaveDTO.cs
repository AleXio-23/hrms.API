namespace hrms.Domain.Models.Vacations.UnpayedLeave
{
    public class UnpayedLeaveDTO
    {
        public int? Id { get; set; }

        public int UserId { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public int CountDays { get; set; }

        public bool? Approved { get; set; }

        public int? ApprovedByUserId { get; set; }

        public string? Comment { get; set; }
    }
}