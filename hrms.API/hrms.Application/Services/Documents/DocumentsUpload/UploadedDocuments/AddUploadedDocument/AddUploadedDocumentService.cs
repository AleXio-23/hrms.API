using AutoMapper;
using hrms.Domain.Models.Documents;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.AspNetCore.Http;

namespace hrms.Application.Services.Documents.DocumentsUpload.UploadedDocuments.AddUploadedDocument
{
    public class AddUploadedDocumentService : IAddUploadedDocumentService
    {
        private readonly IRepository<UploadedDocument> _uploadedDocumentRepository;
        private readonly IMapper _mapper;

        public AddUploadedDocumentService(IRepository<UploadedDocument> uploadedDocumentRepository, IMapper mapper)
        {
            _uploadedDocumentRepository = uploadedDocumentRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<UploadedDocumentDTO>> Execute(UploadedDocumentRequest uploadedDocumentRequest, CancellationToken cancellationToken)
        {
            if (uploadedDocumentRequest.Document != null && uploadedDocumentRequest.Document.Length > 0)
            {
                var fileName = uploadedDocumentRequest.Document.FileName;
                string base64String = await ConvertIFormFileToBase64String(uploadedDocumentRequest.Document).ConfigureAwait(false);

                var newDocument = new UploadedDocument()
                {
                    UploadDate = uploadedDocumentRequest.UploadDate ?? DateTime.Now,
                    DocumentBase64String = base64String,
                    DocumentName = fileName,
                    IsActive = true
                };

                var result = await _uploadedDocumentRepository.Add(newDocument, cancellationToken).ConfigureAwait(false);
                var resultDto = _mapper.Map<UploadedDocumentDTO>(result);

                return ServiceResult<UploadedDocumentDTO>.SuccessResult(resultDto);
            }
            throw new ArgumentException("Document to upload not provided");
        }

        private static async Task<string> ConvertIFormFileToBase64String(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream).ConfigureAwait(false);
            byte[] fileBytes = memoryStream.ToArray();
            string base64String = Convert.ToBase64String(fileBytes);
            return base64String;
        }
    }
}
