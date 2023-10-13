using hrms.Application.Infranstructure.Interfaces.UserInterfaces;
using hrms.Application.Services.Vacation.SickLeaves.Management.GetSickLeave;
using hrms.Domain.Models.Vacations;
using hrms.Domain.Models.Vacations.SickLeave;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Vacation.SickLeaves.Management.ApproveOrNotSickLeaves
{
    internal class ApproveOrNotSickLeavesService : IApproveOrNotSickLeavesService
    {
        private readonly IRepository<SickLeaf> _sickLeaveRepository;
        private readonly IGetCurrentUserIdService _getCurrentUserIdService;
        private readonly IGetSickLeaveService _getSickLeaveService;

        public ApproveOrNotSickLeavesService(IRepository<SickLeaf> sickLeaveRepository, IGetCurrentUserIdService getCurrentUserIdService, IGetSickLeaveService getSickLeaveService)
        {
            _sickLeaveRepository = sickLeaveRepository;
            _getCurrentUserIdService = getCurrentUserIdService;
            _getSickLeaveService = getSickLeaveService;
        }

        public async Task<ServiceResult<SickLeaveDTOWithUserDTO>> Execute(ApproveOrNotLeavesRequest request, CancellationToken cancellationToken)
        {
            var getSickLeave = await _sickLeaveRepository.Where(x => x.Id == request.LeaveId).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Sick leave on id {request.LeaveId} not found");
            var getAuthorisedUserId = _getCurrentUserIdService.Execute();

            if (getSickLeave.Approved != null)
            {
                throw new ArgumentException($"This sick leave request is {(getSickLeave.Approved ?? false ? "already approved" : "denied already")}");

            }

            getSickLeave.Approved = request.IsApproved;
            getSickLeave.Comment = request.Comment;
            getSickLeave.ApprovedByUserId = getAuthorisedUserId;

            var updateResult = await _sickLeaveRepository.Update(getSickLeave, cancellationToken).ConfigureAwait(false);

            var result = (await _getSickLeaveService.Execute(updateResult.Id, cancellationToken).ConfigureAwait(false)).Data ?? throw new NotFoundException($"Error occured while getting sick leave");

            return ServiceResult<SickLeaveDTOWithUserDTO>.SuccessResult(result);
        }
    }
}
