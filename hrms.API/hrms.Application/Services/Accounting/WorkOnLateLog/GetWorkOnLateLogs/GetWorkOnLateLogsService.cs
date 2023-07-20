using AutoMapper;
using hrms.Domain.Models.Accounting;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Accounting.WorkOnLateLog.GetWorkOnLateLogs
{
    public class GetWorkOnLateLogsService : IGetWorkOnLateLogsService
    {
        private readonly IRepository<Persistance.Entities.WorkOnLateLog> _workOnLateLogsRepository;
        private readonly IMapper _mapper;

        public GetWorkOnLateLogsService(IRepository<Persistance.Entities.WorkOnLateLog> workOnLateLogsRepository, IMapper mapper)
        {
            _workOnLateLogsRepository = workOnLateLogsRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<WorkOnLateLogDTO>>> Execute(WorkOnLateLogFilter filter, CancellationToken cancellationToken)
        {
            var query = _workOnLateLogsRepository.GetAllAsQueryable();
            if (filter.UserId != null && filter.UserId > 0)
            {
                query = query.Where(x => x.UserId == filter.UserId);
            }
            if (filter.WorkingTraceReportId != null && filter.WorkingTraceReportId > 0)
            {
                query = query.Where(x => x.WorkingTraceReportId == filter.WorkingTraceReportId);
            }
            if (!string.IsNullOrEmpty(filter.Comment))
            {
                query = query.Where(x => x.Comment != null && x.Comment.Contains(filter.Comment));
            }
            if (filter.IsHonorable != null)
            {
                query = query.Where(x => x.IsHonorable == filter.IsHonorable);
            }
            if (filter.LogDateStart != null)
            {
                query = query.Where(x => x.LogDate >= filter.LogDateStart);
            }
            if (filter.LogDateEnd != null)
            {
                query = query.Where(x => x.LogDate <= filter.LogDateEnd);
            }

            var result = await query.Select(x => _mapper.Map<WorkOnLateLogDTO>(x)).ToListAsync(cancellationToken);

            return ServiceResult<List<WorkOnLateLogDTO>>.SuccessResult(result);
        }
    }
}
