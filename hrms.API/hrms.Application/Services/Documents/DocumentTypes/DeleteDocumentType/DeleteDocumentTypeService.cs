using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Documents.DocumentTypes.DeleteDocumentType
{
    public class DeleteDocumentTypeService : IDeleteDocumentTypeService
    {
        private readonly IRepository<DocumentType> _documentTypeRepository;

        public DeleteDocumentTypeService(IRepository<DocumentType> documentTypeRepository)
        {
            _documentTypeRepository = documentTypeRepository;
        }

        public async Task<ServiceResult<bool>> Execute(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Wrong id value");
            }
            var getDocType = await _documentTypeRepository.Get(id, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Document type on id {id} not found");
            getDocType.IsActive = false;

            await _documentTypeRepository.Update(getDocType, cancellationToken).ConfigureAwait(false);
            return ServiceResult<bool>.SuccessResult(true);
        }
    }
}
