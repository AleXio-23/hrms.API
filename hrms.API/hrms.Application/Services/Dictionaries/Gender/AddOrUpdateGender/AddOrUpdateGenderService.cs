using AutoMapper;
using hrms.Domain.Models.Dictionary.Gender;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Gender.AddOrUpdateGender
{
    public class AddOrUpdateGenderService : IAddOrUpdateGenderService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Persistance.Entities.Gender> _genderRepository;

        public AddOrUpdateGenderService(IMapper mapper,
                                        IRepository<Persistance.Entities.Gender> genderRepository)
        {
            _mapper = mapper;
            _genderRepository = genderRepository;
        }

        public async Task<ServiceResult<GenderDTO>> Execute(GenderDTO gender, CancellationToken cancellationToken)
        {
            if (gender.Id == null || gender.Id < 1)
            {
                var newGender = _mapper.Map<Persistance.Entities.Gender>(gender);
                var addResult = await _genderRepository.Add(newGender, cancellationToken).ConfigureAwait(false);
                var returnResult = _mapper.Map<GenderDTO>(addResult);

                return new ServiceResult<GenderDTO>()
                {
                    Success = true,
                    ErrorOccured = false,
                    Data = returnResult
                };
            }
            else if (gender.Id > 1)
            {
                var getGender = await _genderRepository.Get(gender.Id ?? -1, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Record on Id:{gender.Id} not found");
                getGender.Value = gender.Value;
                getGender.Description = gender.Description;
                getGender.IsActive = getGender.IsActive;

                var saveResult = await _genderRepository.Update(getGender, cancellationToken).ConfigureAwait(false);
                var resultDto = _mapper.Map<GenderDTO>(saveResult);
                return new ServiceResult<GenderDTO>()
                {
                    Success = true,
                    ErrorOccured = false,
                    Data = resultDto
                };

            }
            else
            {
                throw new Exception("Unexpected error occured");
            }
        }
    }
}
