using AutoMapper;
using hrms.Domain.Models.Vacations.Location;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Locations.State.AddOrUpdateState
{
    public class AddOrUpdateStateService : IAddOrUpdateStateService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Persistance.Entities.State> _stateRepository;

        public AddOrUpdateStateService(IMapper mapper, IRepository<Persistance.Entities.State> stateRepository)
        {
            _mapper = mapper;
            _stateRepository = stateRepository;
        }

        public async Task<ServiceResult<StateDTO>> Execute(StateDTO stateDTO, CancellationToken cancellationToken)
        {
            if (stateDTO.Id == null || stateDTO.Id < 1)
            {
                var newState = _mapper.Map<Persistance.Entities.State>(stateDTO);
                var addResult = await _stateRepository.Add(newState, cancellationToken).ConfigureAwait(false);
                var returnResult = _mapper.Map<StateDTO>(addResult);

                return ServiceResult<StateDTO>.SuccessResult(returnResult);
            }
            else if (stateDTO.Id > 1)
            {
                var getState = await _stateRepository.Get(stateDTO.Id ?? -1, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Record on Id:{stateDTO.Id} not found");

                getState.Code = stateDTO.Code;
                getState.Name = stateDTO.Name;
                getState.CountryId = stateDTO.CountryId;
                getState.SortIndex = stateDTO.SortIndex;
                getState.IsActive = stateDTO.IsActive;

                var saveResult = await _stateRepository.Update(getState, cancellationToken).ConfigureAwait(false);
                var resultDto = _mapper.Map<StateDTO>(saveResult);

                return ServiceResult<StateDTO>.SuccessResult(resultDto);

            }
            else
            {
                throw new Exception("Unexpected error occured");
            }
        }
    }
}
