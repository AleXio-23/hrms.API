namespace hrms.Domain.Models.Documents
{
    public class DocumentTypeDTO
    {
        public int? Id { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; } = null!;

        public bool? IsDocumentSizeLimited { get; set; }

        public int? MaxDocumentSizeInMbsToUpload { get; set; }

        public bool? IsActive { get; set; }
    }
}
