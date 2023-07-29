using hrms.Domain.Models.Documents;
using hrms.Domain.Models.Vacations.SickLeave;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Vacation.SickLeaves.Management.GetSickLeave
{
    public class GetSickLeaveService : IGetSickLeaveService
    {
        private readonly IRepository<SickLeaf> _sickLeavesRepository;

        public GetSickLeaveService(IRepository<SickLeaf> sickLeavesRepository)
        {
            _sickLeavesRepository = sickLeavesRepository;
        }

        public async Task<ServiceResult<SickLeaveDTOWithUserDTO>> Execute(int id, CancellationToken cancellationToken)
        {
            var result = await _sickLeavesRepository.Where(x => x.Id == id)
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
               }).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false)
               ?? throw new NotFoundException($"Sick leave on id {id} not found");

            return ServiceResult<SickLeaveDTOWithUserDTO>.SuccessResult(result);
        }
    }
}
