using AutoMapper;
using hrms.Domain.Models.Accounting;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Accounting.LogLateFromBreak.GetLogsLateFromBreak
{
    public class GetLogsLateFromBreakService : IGetLogsLateFromBreakService
    {
        private readonly IRepository<LateFromBreak> _lateFromBreakRepository;
        private readonly IMapper _mapper;

        public GetLogsLateFromBreakService(IRepository<LateFromBreak> lateFromBreakRepository, IMapper mapper)
        {
            _lateFromBreakRepository = lateFromBreakRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<LateFromBreakDTO>>> Execute(LateFromBreakFilter filter, CancellationToken cancellationToken)
        {
            var query = _lateFromBreakRepository.GetAllAsQueryable();
            if (filter.UserId != null && filter.UserId > 0)
            {
                query = query.Where(x => x.UserId == filter.UserId);
            }
            if (filter.WorkingTraceReportId != null && filter.WorkingTraceReportId > 0)
            {
                query = query.Where(x => x.WorkingTraceReportId == filter.WorkingTraceReportId);
            }
            if (filter.TraceWorkingId != null && filter.TraceWorkingId > 0)
            {
                query = query.Where(x => x.TraceWorkingId == filter.TraceWorkingId);
            }
            if (!string.IsNullOrEmpty(filter.Comment))
            {
                query = query.Where(x => x.Comment != null && x.Comment.Contains(filter.Comment));
            }
            if (filter.IsHonorable != null)
            {
                query = query.Where(x => x.IsHonorable == filter.IsHonorable);
            }
            if (filter.LogStartDate != null)
            {
                query = query.Where(x => x.LogDate >= filter.LogStartDate);
            }
            if (filter.LogEndDate != null)
            {
                query = query.Where(x => x.LogDate <= filter.LogEndDate);
            }

            var result = await query.Select(x => _mapper.Map<LateFromBreakDTO>(x)).ToListAsync(cancellationToken);

            return ServiceResult<List<LateFromBreakDTO>>.SuccessResult(result);
        }
    }
}
