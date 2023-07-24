using AutoMapper;
using hrms.Domain.Models.Documents;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Documents.DocumentTypes.GetDocumentTypes
{
    public class GetDocumentTypesService : IGetDocumentTypesService
    {
        private readonly IRepository<DocumentType> _documentTypeRepository;
        private readonly IMapper _mapper;

        public GetDocumentTypesService(IRepository<DocumentType> documentTypeRepository, IMapper mapper)
        {
            _documentTypeRepository = documentTypeRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResult<List<DocumentTypeDTO>>> Execute(DocumentTypeFilter filter, CancellationToken cancellationToken)
        {
            var query = _documentTypeRepository.GetAllAsQueryable();
            if (!string.IsNullOrEmpty(filter.Code))
            {
                query = query.Where(x => x.Code != null && x.Code.Contains(filter.Code));
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => x.Name.Contains(filter.Name));
            }
            if (filter.IsDocumentSizeLimited != null)
            {
                query = query.Where(x => x.IsDocumentSizeLimited == filter.IsDocumentSizeLimited);
            }
            if (filter.MaxDocumentSizeInMbsToUpload != null)
            {
                query = query.Where(x => x.MaxDocumentSizeInMbsToUpload.Equals(filter.MaxDocumentSizeInMbsToUpload));
            }
            if (filter.IsActive != null)
            {
                query = query.Where(x => x.IsActive.Equals(filter.IsActive));
            }

            var result = await query
                .Select(x => _mapper.Map<DocumentTypeDTO>(x))
                .ToListAsync(cancellationToken).ConfigureAwait(false)
                ?? new List<DocumentTypeDTO>();

            return ServiceResult<List<DocumentTypeDTO>>.SuccessResult(result);
        }
    }
}
