namespace hrms.Domain.Models.Documents
{
    public class DocumentTypeFilter
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public bool? IsDocumentSizeLimited { get; set; }
        public int? MaxDocumentSizeInMbsToUpload { get; set; }
        public bool? IsActive { get; set; }
    }
}
