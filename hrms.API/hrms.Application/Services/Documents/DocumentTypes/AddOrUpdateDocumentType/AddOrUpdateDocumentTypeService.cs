using AutoMapper;
using hrms.Domain.Models.Documents;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Documents.DocumentTypes.AddOrUpdateDocumentType
{
    public class AddOrUpdateDocumentTypeService : IAddOrUpdateDocumentTypeService
    {
        private readonly IRepository<DocumentType> _documentTypeRepository;
        private readonly IMapper _mapper;

        public AddOrUpdateDocumentTypeService(IRepository<DocumentType> documentTypeRepository, IMapper mapper)
        {
            _documentTypeRepository = documentTypeRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<DocumentTypeDTO>> Execute(DocumentTypeDTO documentTypeDTO, CancellationToken cancellationToken)
        {
            if (documentTypeDTO.Id == null || documentTypeDTO.Id <= 0)
            {
                var newType = new DocumentType()
                {
                    Code = documentTypeDTO.Code,
                    Name = documentTypeDTO.Name ?? throw new ArgumentException("Document type name must be provided"),
                    IsDocumentSizeLimited = documentTypeDTO.IsDocumentSizeLimited,
                    MaxDocumentSizeInMbsToUpload = documentTypeDTO.MaxDocumentSizeInMbsToUpload ?? 1024,
                    IsActive = true
                };

                var addNewType = await _documentTypeRepository.Add(newType, cancellationToken).ConfigureAwait(false);
                var resultDto = _mapper.Map<DocumentTypeDTO>(addNewType);

                return ServiceResult<DocumentTypeDTO>.SuccessResult(resultDto);
            }
            else
            {
                var getExistingDocumentType = await _documentTypeRepository
                    .Get(documentTypeDTO.Id ?? throw new ArgumentException("Wrong Id value"), cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Document type in id {documentTypeDTO.Id} not found");

                getExistingDocumentType.Code = documentTypeDTO.Code;
                getExistingDocumentType.Name = documentTypeDTO.Name ?? throw new ArgumentException("Document type name must be provided");
                getExistingDocumentType.IsDocumentSizeLimited = documentTypeDTO.IsDocumentSizeLimited;
                getExistingDocumentType.MaxDocumentSizeInMbsToUpload = documentTypeDTO.MaxDocumentSizeInMbsToUpload ?? 1024;
                getExistingDocumentType.IsActive = true;

                await _documentTypeRepository.Update(getExistingDocumentType, cancellationToken).ConfigureAwait(false);
                return ServiceResult<DocumentTypeDTO>.SuccessResult(documentTypeDTO);
            }
        }
    }
}
