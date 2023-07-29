using hrms.Application.Services.Vacation.PayedLeaves.Management.GetPayedLeave;
using hrms.Domain.Models.Vacations;
using hrms.Domain.Models.Vacations.PayedLeave;
using hrms.Infranstructure.Services.CurrentUserId;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Vacation.PayedLeaves.Management.ApproveOrNotPayedLeaves
{
    public class ApproveOrNotPayedLeavesService : IApproveOrNotPayedLeavesService
    {
        private readonly IRepository<PayedLeaf> _payedLeavesRepository;
        private readonly IGetCurrentUserIdService _getCurrentUserIdService;
        private readonly IGetPayedLeaveService _getPayedLeaveService;

        public ApproveOrNotPayedLeavesService(IRepository<PayedLeaf> payedLeavesRepository, IGetCurrentUserIdService getCurrentUserIdService, IGetPayedLeaveService getPayedLeaveService)
        {
            _payedLeavesRepository = payedLeavesRepository;
            _getCurrentUserIdService = getCurrentUserIdService;
            _getPayedLeaveService = getPayedLeaveService;
        }

        public async Task<ServiceResult<PayedLeaveDTOWithUserDTO>> Execute(ApproveOrNotLeavesRequest request, CancellationToken cancellationToken)
        {
            var getPayedLeave = await _payedLeavesRepository.Where(x => x.Id == request.LeaveId).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Payed leave on id {request.LeaveId} not found");
            var getAuthorisedUserId = _getCurrentUserIdService.Execute();

            if (getPayedLeave.Approved != null)
            {
                throw new ArgumentException($"This payed leave request is {(getPayedLeave.Approved ?? false ? "already approved" : "denied already")}");

            }

            getPayedLeave.Approved = request.IsApproved;
            getPayedLeave.Comment = request.Comment;
            getPayedLeave.ApprovedByUserId = getAuthorisedUserId;

            var updateResult = await _payedLeavesRepository.Update(getPayedLeave, cancellationToken).ConfigureAwait(false);

            var result = (await _getPayedLeaveService.Execute(updateResult.Id, cancellationToken).ConfigureAwait(false)).Data ?? throw new NotFoundException($"Error occured while getting payed leave");

            return ServiceResult<PayedLeaveDTOWithUserDTO>.SuccessResult(result);
        }
    }
}
