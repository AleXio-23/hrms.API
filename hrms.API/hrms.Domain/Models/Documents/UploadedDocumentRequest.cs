using Microsoft.AspNetCore.Http;

namespace hrms.Domain.Models.Documents
{
    public class UploadedDocumentRequest
    {
        public IFormFile? Document { get; set; }
        public DateTime? UploadDate { get; set; }
    }
}
