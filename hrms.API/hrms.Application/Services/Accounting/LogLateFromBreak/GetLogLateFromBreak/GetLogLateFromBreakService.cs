using AutoMapper;
using hrms.Domain.Models.Accounting;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Accounting.LogLateFromBreak.GetLogLateFromBreak
{
    public class GetLogLateFromBreakService : IGetLogLateFromBreakService
    {
        private readonly IRepository<LateFromBreak> _lateFromBreakRepository;
        private readonly IMapper _mapper;

        public GetLogLateFromBreakService(IRepository<LateFromBreak> lateFromBreakRepository, IMapper mapper)
        {
            _lateFromBreakRepository = lateFromBreakRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<LateFromBreakDTO>> Execute(int id, CancellationToken cancellationToken)
        {
            var getLateBreakLog = await _lateFromBreakRepository.Get(id, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Late break log on id {id} not found");

            var getMappedDto = _mapper.Map<LateFromBreakDTO>(getLateBreakLog);

            return ServiceResult<LateFromBreakDTO>.SuccessResult(getMappedDto);
        }
    }
}
