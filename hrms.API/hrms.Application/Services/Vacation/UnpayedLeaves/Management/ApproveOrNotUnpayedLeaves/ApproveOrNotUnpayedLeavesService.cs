using hrms.Application.Services.Vacation.UnpayedLeaves.Management.GetUnpayedLeave;
using hrms.Domain.Models.Vacations;
using hrms.Domain.Models.Vacations.UnpayedLeave;
using hrms.Infranstructure.Services.CurrentUserId;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Vacation.UnpayedLeaves.Management.ApproveOrNotUnpayedLeaves
{
    public class ApproveOrNotUnpayedLeavesService : IApproveOrNotUnpayedLeavesService
    {
        private readonly IRepository<UnpayedLeaf> _unpayedLeavesRepository;
        private readonly IGetCurrentUserIdService _getCurrentUserIdService;
        private readonly IGetUnpayedLeaveService _getUnpayedLeaveService;

        public ApproveOrNotUnpayedLeavesService(IRepository<UnpayedLeaf> unpayedLeavesRepository, IGetCurrentUserIdService getCurrentUserIdService, IGetUnpayedLeaveService getUnpayedLeaveService)
        {
            _unpayedLeavesRepository = unpayedLeavesRepository;
            _getCurrentUserIdService = getCurrentUserIdService;
            _getUnpayedLeaveService = getUnpayedLeaveService;
        }

        public async Task<ServiceResult<UnpayedLeaveDTOWithUserDTO>> Execute(ApproveOrNotLeavesRequest request, CancellationToken cancellationToken)
        {
            var getPayedLeave = await _unpayedLeavesRepository.Where(x => x.Id == request.LeaveId && x.UserId == request.UserId).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Unpayed leave on id {request.LeaveId} for user with id {request.UserId} not found");
            var getAuthorisedUserId = _getCurrentUserIdService.Execute();

            if (getPayedLeave.Approved != null)
            {
                throw new ArgumentException($"This unpayed leave request is {(getPayedLeave.Approved ?? false ? "already approved" : "denied already")}");

            }

            getPayedLeave.Approved = request.IsApproved;
            getPayedLeave.Comment = request.Comment;
            getPayedLeave.ApprovedByUserId = getAuthorisedUserId;

            var updateResult = await _unpayedLeavesRepository.Update(getPayedLeave, cancellationToken).ConfigureAwait(false);

            var result = (await _getUnpayedLeaveService.Execute(updateResult.Id, cancellationToken).ConfigureAwait(false)).Data ?? throw new NotFoundException($"Error occured while getting payed leave");

            return ServiceResult<UnpayedLeaveDTOWithUserDTO>.SuccessResult(result);
        }
    }
}
