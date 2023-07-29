using hrms.Domain.Models.Documents;
using hrms.Domain.Models.User;

namespace hrms.Domain.Models.Vacations.SickLeave
{
    public class SickLeaveDTOWithUserDTO
    {
        public int? Id { get; set; }

        public int UserId { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public int? CountDays { get; set; }

        public int? DocumentId { get; set; }

        public bool? Approved { get; set; }

        public int? ApprovedByUserId { get; set; }

        public string? Comment { get; set; }

        public virtual UserProfileDTO? ApprovedByUser { get; set; }

        public virtual UserUploadedDocumentDTO? Document { get; set; }

        public virtual UserProfileDTO User { get; set; } = null!;
    }
}
