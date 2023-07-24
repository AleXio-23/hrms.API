using Microsoft.AspNetCore.Http;

namespace hrms.Domain.Models.Documents
{
    public class UploadDocumentRequest
    {
        public IFormFile? File { get; set; }
        public int? DocumentTypeId { get; set; }
        public string? DocumentTypeIfNotFoundInDicitonary { get; set; }
    }
}
