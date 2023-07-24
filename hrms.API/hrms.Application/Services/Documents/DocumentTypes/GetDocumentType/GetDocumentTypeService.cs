using AutoMapper;
using hrms.Domain.Models.Documents;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Documents.DocumentTypes.GetDocumentType
{
    public class GetDocumentTypeService : IGetDocumentTypeService
    {
        private readonly IRepository<DocumentType> _documentTypeRepository;
        private readonly IMapper _mapper;

        public GetDocumentTypeService(IRepository<DocumentType> documentTypeRepository, IMapper mapper)
        {
            _documentTypeRepository = documentTypeRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResult<DocumentTypeDTO>> Execute(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Wrong id value");
            }
            var getDocumentType = await _documentTypeRepository
                .Get(id, cancellationToken)
                .ConfigureAwait(false) ?? throw new NotFoundException($"Documenty type on id {id} not found");
            var resultDto = _mapper.Map<DocumentTypeDTO>(getDocumentType);

            return ServiceResult<DocumentTypeDTO>.SuccessResult(resultDto);
        }
    }
}
