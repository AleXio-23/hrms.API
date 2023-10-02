using hrms.Domain.Models.Vacations.GetAllLeavesForUser;
using hrms.Shared.Models;

namespace hrms.Application.Services.Vacation.GetAllLeavesForUser
{
    public interface IGetAllLeavesForUserService
    {
        Task<ServiceResult<GetAllLeavesResponse>> Execute(GetAllLeavesRequest request, CancellationToken cancellationToken);
    }
}
