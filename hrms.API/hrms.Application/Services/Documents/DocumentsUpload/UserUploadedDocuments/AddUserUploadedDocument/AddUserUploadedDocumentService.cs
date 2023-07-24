using AutoMapper;
using hrms.Domain.Models.Documents;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;

namespace hrms.Application.Services.Documents.DocumentsUpload.UserUploadedDocuments.AddUserUploadedDocument
{
    public class AddUserUploadedDocumentService : IAddUserUploadedDocumentService
    {
        private readonly IRepository<UserUploadedDocument> _userUploadedDocumentRepository;
        private readonly IMapper _mapper;

        public AddUserUploadedDocumentService(IRepository<UserUploadedDocument> userUploadedDocumentRepository, IMapper mapper)
        {
            _userUploadedDocumentRepository = userUploadedDocumentRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<UserUploadedDocumentDTO>> Execute(UserUploadedDocumentDTO userUploadedDocumentDTO, CancellationToken cancellationToken)
        {
            var newDocument = new UserUploadedDocument()
            {
                UploadedByUserId = userUploadedDocumentDTO.UploadedByUserId ?? throw new ArgumentException("Document ulploader user id not found"),
                UploadDate = userUploadedDocumentDTO.UploadDate ?? DateTime.Now,
                DocumentId = userUploadedDocumentDTO.DocumentId ?? throw new ArgumentException("Document Id must be passed"),
                DocumentTypeId = userUploadedDocumentDTO.DocumentTypeId,
                DocumentTypeIfNotFoundInDicitonary = userUploadedDocumentDTO.DocumentTypeIfNotFoundInDicitonary
            };

            var result = await _userUploadedDocumentRepository.Add(newDocument, cancellationToken).ConfigureAwait(false);
            var resultDto = _mapper.Map<UserUploadedDocumentDTO>(result);

            return ServiceResult<UserUploadedDocumentDTO>.SuccessResult(resultDto);
        }
    }
}
