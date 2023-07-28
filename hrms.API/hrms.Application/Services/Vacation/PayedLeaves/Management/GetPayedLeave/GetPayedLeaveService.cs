using hrms.Domain.Models.Vacations.PayedLeave;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Vacation.PayedLeaves.Management.GetPayedLeave
{
    public class GetPayedLeaveService : IGetPayedLeaveService
    {
        private readonly IRepository<PayedLeaf> _payedLeavesRepository;

        public GetPayedLeaveService(IRepository<PayedLeaf> payedLeavesRepository)
        {
            _payedLeavesRepository = payedLeavesRepository;
        }

        public async Task<ServiceResult<PayedLeaveDTOWithUserDTO>> Execute(int id, CancellationToken cancellationToken)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var result = await _payedLeavesRepository.Where(x => x.Id == id)
                 .Include(x => x.User)
                 .ThenInclude(u => u.UserProfile)
                 .Include(x => x.ApprovedByUser)
                 .ThenInclude(u => u.UserProfile)
                 .Select(x => new PayedLeaveDTOWithUserDTO()
                 {
                     Id = x.Id,
                     UserId = x.UserId,
                     DateStart = x.DateStart,
                     DateEnd = x.DateEnd,
                     CountDays = x.CountDays,
                     PayBeforeHeadEnabled = x.PayBeforeHeadEnabled,
                     Approved = x.Approved,
                     ApprovedByUserId = x.ApprovedByUserId,
                     Comment = x.Comment,
                     UserProfileDTO = x.User != null && x.User.UserProfile != null ? new Domain.Models.User.UserProfileDTO()
                     {
                         Id = x.User.UserProfile.UserId,
                         UserId = x.User.UserProfile.UserId,
                         Firstname = x.User.UserProfile.Firstname,
                         Lastname = x.User.UserProfile.Lastname,
                         PhoneNumber1 = x.User.UserProfile.PhoneNumber1,
                         PhoneNumber2 = x.User.UserProfile.PhoneNumber2,
                         BirthDate = x.User.UserProfile.BirthDate,
                         PersonalNumber = x.User.UserProfile.PersonalNumber,
                         GenderId = x.User.UserProfile.GenderId,
                         RegisterDate = x.User.UserProfile.RegisterDate
                     } : null,
                     ApprovedByUserProfileDTO = x.ApprovedByUser != null && x.ApprovedByUser.UserProfile != null ? new Domain.Models.User.UserProfileDTO()
                     {
                         Id = x.ApprovedByUser.UserProfile.UserId,
                         UserId = x.ApprovedByUser.UserProfile.UserId,
                         Firstname = x.ApprovedByUser.UserProfile.Firstname,
                         Lastname = x.ApprovedByUser.UserProfile.Lastname,
                         PhoneNumber1 = x.ApprovedByUser.UserProfile.PhoneNumber1,
                         PhoneNumber2 = x.ApprovedByUser.UserProfile.PhoneNumber2,
                         BirthDate = x.ApprovedByUser.UserProfile.BirthDate,
                         PersonalNumber = x.ApprovedByUser.UserProfile.PersonalNumber,
                         GenderId = x.ApprovedByUser.UserProfile.GenderId,
                         RegisterDate = x.ApprovedByUser.UserProfile.RegisterDate
                     } : null

                 })
                 .FirstOrDefaultAsync(cancellationToken)
                 .ConfigureAwait(false) ?? throw new NotFoundException($"Payed leave on id {id} not found");
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            return ServiceResult<PayedLeaveDTOWithUserDTO>.SuccessResult(result);
        }
    }
}
