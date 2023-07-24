namespace hrms.Domain.Models.Documents
{
    public class UploadedDocumentDTO
    {
        public int? Id { get; set; }

        public DateTime? UploadDate { get; set; }

        public string? DocumentBase64String { get; set; }

        public string? DocumentName { get; set; }

        public bool? IsActive { get; set; }
    }
}
