using AutoMapper;
using hrms.Domain.Models.Accounting;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;

namespace hrms.Application.Services.Accounting.LogLateFromBreak.AddLogLateFromBreak
{
    public class AddLogLateFromBreakService : IAddLogLateFromBreakService
    {
        private readonly IRepository<LateFromBreak> _lateFromBreakRepository;
        private readonly IMapper _mapper;

        public AddLogLateFromBreakService(IRepository<LateFromBreak> lateFromBreakRepository, IMapper mapper)
        {
            _lateFromBreakRepository = lateFromBreakRepository;
            _mapper = mapper;
        }

        public Task<ServiceResult<LateFromBreakDTO>> Execute(LateFromBreakDTO lateFromBreakDTO, CancellationToken cancellationToken)
        {
            var addNewLateFromBreakLog = new LateFromBreak()
            {
                UserId = lateFromBreakDTO.UserId ?? throw new ArgumentException("User Id must be provided to log break late record"),
                WorkingTraceReportId = lateFromBreakDTO.WorkingTraceReportId ?? throw new ArgumentException("WorkingTraceReportId must be provided to log break late record")


            };

            return null;
        }
    }
}
