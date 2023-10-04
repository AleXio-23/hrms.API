using AutoMapper;
using hrms.Domain.Models.Dictionary.Gender;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Gender.GetGender
{
    public class GetGenderService : IGetGenderService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Persistance.Entities.Gender> _genderRepository;

        public GetGenderService(IRepository<Persistance.Entities.Gender> genderRepository,
                                IMapper mapper)
        {
            _genderRepository = genderRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<GenderDTO>> Execute(int id, CancellationToken cancellationToken)
        {
            if (id < 1)
            {
                throw new ArgumentException("Wrong id value");
            }

            var getGender = await _genderRepository.Get(id, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Record with Id: {id} not found.");

            var genderDto = _mapper.Map<GenderDTO>(getGender);

            return ServiceResult<GenderDTO>.SuccessResult(genderDto);
        }
    }
}
