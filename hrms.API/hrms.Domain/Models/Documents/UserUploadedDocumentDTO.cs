namespace hrms.Domain.Models.Documents
{
    public class UserUploadedDocumentDTO
    {
        public int? Id { get; set; }

        public int? UploadedByUserId { get; set; }

        public DateTime? UploadDate { get; set; }

        public int? DocumentTypeId { get; set; }

        public string? DocumentTypeIfNotFoundInDicitonary { get; set; }

        public int? DocumentId { get; set; }
    }
}
