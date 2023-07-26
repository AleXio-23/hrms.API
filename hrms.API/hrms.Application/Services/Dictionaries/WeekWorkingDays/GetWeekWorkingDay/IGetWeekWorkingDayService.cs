using hrms.Domain.Models.Dictionary.WeekWorkingDays;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.WeekWorkingDays.GetWeekWorkingDay
{
    public interface IGetWeekWorkingDayService
    {
        Task<ServiceResult<WeekWorkingDayDTO>> Execute(int id, CancellationToken cancellationToken);
    }
}
