using hrms.Domain.Models.Dictionary.Departments;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Gender.DeleteGender
{
    public class DeleteGenerService : IDeleteGenerService
    {
        private readonly IRepository<Persistance.Entities.Gender> _genderRepository;

        public DeleteGenerService(IRepository<Persistance.Entities.Gender> genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public async Task<ServiceResult<bool>> Execute(int id, CancellationToken cancellationToken)
        {
            if (id < 1) throw new ArgumentException("Wrong value for id");
            var getGender = await _genderRepository.Get(id, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Gender on id: {id} not found");
            getGender.IsActive = false;
            await _genderRepository.Update(getGender, cancellationToken).ConfigureAwait(false);


            return ServiceResult<bool>.SuccessResult(true);
        }
    }
}
