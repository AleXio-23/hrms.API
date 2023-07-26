using hrms.Domain.Models.Dictionary.WeekWorkingDays;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.WeekWorkingDays.GetWeekWorkingDays
{
    public interface IGetWeekWorkingDaysService
    {
        Task<ServiceResult<List<WeekWorkingDayDTO>>> Execute(CancellationToken cancellationToken);
    }
}
