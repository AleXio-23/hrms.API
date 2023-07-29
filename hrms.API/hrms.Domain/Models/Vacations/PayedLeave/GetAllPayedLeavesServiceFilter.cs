namespace hrms.Domain.Models.Vacations.PayedLeave
{
    public class GetAllPayedLeavesServiceFilter
    {
        public int? UserId { get; set; }
        public string? UserFirstname { get; set; }
        public string? UserLastName { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }

        public int? CountDays { get; set; }

        public bool? PayBeforeHeadEnabled { get; set; }

        public bool? Approved { get; set; }

        public int? ApprovedByUserId { get; set; }
        public string? ApprovedByUserFirstname { get; set; }
        public string? ApprovedByUserLastName { get; set; }

        public string? Comment { get; set; }
    }
}
