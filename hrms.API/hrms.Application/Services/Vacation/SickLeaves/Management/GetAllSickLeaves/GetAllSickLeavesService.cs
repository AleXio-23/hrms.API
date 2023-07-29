using hrms.Domain.Models.Documents;
using hrms.Domain.Models.Vacations.SickLeave;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Vacation.SickLeaves.Management.GetAllSickLeaves
{
    public class GetAllSickLeavesService : IGetAllSickLeavesService
    {
        private readonly IRepository<SickLeaf> _sickLeavesRepository;

        public GetAllSickLeavesService(IRepository<SickLeaf> sickLeavesRepository)
        {
            _sickLeavesRepository = sickLeavesRepository;
        }

        public async Task<ServiceResult<List<SickLeaveDTOWithUserDTO>>> Execute(GetAllSickLeavesServiceFilter filter, CancellationToken cancellationToken)
        {
            var query = _sickLeavesRepository.GetAllAsQueryable();

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
              .ThenInclude(x => x.UserProfile)
              .Include(x => x.ApprovedByUser)
              .ThenInclude(x => x.UserProfile)
              .Include(x => x.Documents)
              .ThenInclude(x => x.Document)
              .Select(sickLeaf => new SickLeaveDTOWithUserDTO()
              {
                  Id = sickLeaf.Id,
                  UserId = sickLeaf.UserId,
                  DateStart = sickLeaf.DateStart,
                  DateEnd = sickLeaf.DateEnd,
                  CountDays = sickLeaf.CountDays,
                  Approved = sickLeaf.Approved,
                  ApprovedByUserId = sickLeaf.ApprovedByUserId,
                  Comment = sickLeaf.Comment,
                  User = sickLeaf.User != null && sickLeaf.User.UserProfile != null ? new Domain.Models.User.UserProfileDTO()
                  {
                      Id = sickLeaf.User.UserProfile.UserId,
                      UserId = sickLeaf.User.UserProfile.UserId,
                      Firstname = sickLeaf.User.UserProfile.Firstname,
                      Lastname = sickLeaf.User.UserProfile.Lastname,
                      PhoneNumber1 = sickLeaf.User.UserProfile.PhoneNumber1,
                      PhoneNumber2 = sickLeaf.User.UserProfile.PhoneNumber2,
                      BirthDate = sickLeaf.User.UserProfile.BirthDate,
                      PersonalNumber = sickLeaf.User.UserProfile.PersonalNumber,
                      GenderId = sickLeaf.User.UserProfile.GenderId,
                      RegisterDate = sickLeaf.User.UserProfile.RegisterDate
                  } : new Domain.Models.User.UserProfileDTO(),
                  ApprovedByUser = sickLeaf.ApprovedByUser != null && sickLeaf.ApprovedByUser.UserProfile != null ? new Domain.Models.User.UserProfileDTO()
                  {
                      Id = sickLeaf.ApprovedByUser.UserProfile.UserId,
                      UserId = sickLeaf.ApprovedByUser.UserProfile.UserId,
                      Firstname = sickLeaf.ApprovedByUser.UserProfile.Firstname,
                      Lastname = sickLeaf.ApprovedByUser.UserProfile.Lastname,
                      PhoneNumber1 = sickLeaf.ApprovedByUser.UserProfile.PhoneNumber1,
                      PhoneNumber2 = sickLeaf.ApprovedByUser.UserProfile.PhoneNumber2,
                      BirthDate = sickLeaf.ApprovedByUser.UserProfile.BirthDate,
                      PersonalNumber = sickLeaf.ApprovedByUser.UserProfile.PersonalNumber,
                      GenderId = sickLeaf.ApprovedByUser.UserProfile.GenderId,
                      RegisterDate = sickLeaf.ApprovedByUser.UserProfile.RegisterDate
                  } : null,
                  Document = sickLeaf.Documents.Select(x => new UserUploadedDocumentDTO()
                  {
                      Id = x.Id,
                      UploadedByUserId = x.UploadedByUserId,
                      UploadDate = x.UploadDate,
                      DocumentTypeId = x.DocumentTypeId,
                      DocumentTypeIfNotFoundInDicitonary = x.DocumentTypeIfNotFoundInDicitonary,
                      DocumentId = x.DocumentId
                  }).ToList()
              }).ToListAsync(cancellationToken).ConfigureAwait(false) ?? new List<SickLeaveDTOWithUserDTO>();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            return ServiceResult<List<SickLeaveDTOWithUserDTO>>.SuccessResult(result);
        }
    }
}
