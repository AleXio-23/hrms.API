using hrms.Domain.Models.Vacations.UnpayedLeave;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Vacation.UnpayedLeaves.Management.GetAllUnpayedLeaves
{
    public class GetAllUnpayedLeavesService : IGetAllUnpayedLeavesService
    {
        private readonly IRepository<UnpayedLeaf> _unpayedLeavesRepository;

        public GetAllUnpayedLeavesService(IRepository<UnpayedLeaf> unpayedLeavesRepository)
        {
            _unpayedLeavesRepository = unpayedLeavesRepository;
        }

        public async Task<ServiceResult<List<UnpayedLeaveDTOWithUserDTO>>> Execute(GetAllUnpayedLeavesServiceFilter filter, CancellationToken cancellationToken)
        {
            var query = _unpayedLeavesRepository.GetAllAsQueryable();

            if (filter.UserId != null && filter.UserId > 0)
            {
                query = query.Where(x => x.UserId == filter.UserId);
            }

            if (!string.IsNullOrEmpty(filter.UserFirstname))
            {
                query = query.Where(x => x.User != null
                                && x.User.UserProfile != null
                                && x.User.UserProfile.Lastname != null
                                && x.User.UserProfile.Lastname.Contains(filter.UserFirstname));
            }

            if (!string.IsNullOrEmpty(filter.UserLastName))
            {
                query = query.Where(x => x.User != null
                                && x.User.UserProfile != null
                                && x.User.UserProfile.Lastname != null
                                && x.User.UserProfile.Lastname.Contains(filter.UserLastName));
            }
            if (filter.DateStart != null)
            {
                query = query.Where(x => x.DateStart >= filter.DateStart);
            }
            if (filter.DateEnd != null)
            {
                query = query.Where(x => x.DateEnd <= filter.DateEnd);
            }
            if (filter.CountDays > 0)
            {
                query = query.Where(x => x.CountDays == filter.CountDays);
            }

            if (filter.Approved.HasValue)
            {
                query = query.Where(x => x.Approved == filter.Approved.Value);
            }

            if (filter.ApprovedByUserId.HasValue && filter.ApprovedByUserId.Value > 0)
            {
                query = query.Where(x => x.ApprovedByUserId == filter.ApprovedByUserId.Value);
            }

            if (!string.IsNullOrEmpty(filter.ApprovedByUserFirstname))
            {
                query = query.Where(x => x.ApprovedByUser != null
                                && x.ApprovedByUser.UserProfile != null
                                && x.ApprovedByUser.UserProfile.Firstname != null
                                && x.ApprovedByUser.UserProfile.Firstname.Contains(filter.ApprovedByUserFirstname));
            }

            if (!string.IsNullOrEmpty(filter.ApprovedByUserLastName))
            {
                query = query
                    .Where(x => x.ApprovedByUser != null
                                && x.ApprovedByUser.UserProfile != null
                                && x.ApprovedByUser.UserProfile.Lastname != null
                                && x.ApprovedByUser.UserProfile.Lastname.Contains(filter.ApprovedByUserLastName));
            }

            if (!string.IsNullOrEmpty(filter.Comment))
            {
                query = query.Where(x => x.Comment != null && x.Comment.Contains(filter.Comment));
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var result = await query
                .Include(x => x.User)
                .ThenInclude(u => u.UserProfile)
                .Include(x => x.ApprovedByUser)
                .ThenInclude(u => u.UserProfile)
                .Select(x => new UnpayedLeaveDTOWithUserDTO()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    DateStart = x.DateStart,
                    DateEnd = x.DateEnd,
                    CountDays = x.CountDays,
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
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false) ?? new List<UnpayedLeaveDTOWithUserDTO>();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            return ServiceResult<List<UnpayedLeaveDTOWithUserDTO>>.SuccessResult(result);
        }
    }
}
