using AutoMapper;
using hrms.Domain.Models.Dictionary;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Dictionaries.Locations
{
    public class GetLocationWithGenerationsService : IGetLocationWithGenerationsService
    {
        private readonly IRepository<Persistance.Entities.Country> _countryRepository;
        private readonly IMapper _mapper;

        public GetLocationWithGenerationsService(IRepository<Persistance.Entities.Country> countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<DefaultMultiLevelDictionaryResponse>>> Execute(CancellationToken cancellation)
        {

            var getCountries = await _countryRepository
                .Where(x => x.IsActive == true)
                .Select(x => new DefaultMultiLevelDictionaryResponse
                {
                    Id = x.Id,
                    ParentId = null,
                    Code = x.Code,
                    Name = x.Name,
                    HasNextLevel = x.HasStates,
                    NextLevelChildName = x.HasStates ? "CHILD_STATE" : "CHILD_CITY",
                    SortIndex = x.SortIndex,
                    IsActive = x.IsActive,

                    Children = x.HasStates
                        ? x.States
                            .Where(s => s.IsActive == true)
                            .Select(s => new DefaultMultiLevelDictionaryResponse
                            {
                                Id = s.Id,
                                ParentId = s.CountryId,
                                Code = s.Code,
                                Name = s.Name,
                                HasNextLevel = true,
                                NextLevelChildName = "CHILD_CITY",
                                SortIndex = s.SortIndex,
                                IsActive = s.IsActive,
                                Children = s.Cities
                                    .Where(c => c.IsActive == true)
                                    .Select(c => new DefaultMultiLevelDictionaryResponse
                                    {
                                        Id = c.Id,
                                        ParentId = c.StateId ?? s.Id,
                                        HasNextLevel = false,
                                        NextLevelChildName = null,
                                        Code = c.Code,
                                        Name = c.Name,
                                        SortIndex = c.SortIndex,
                                        IsActive = c.IsActive,
                                    })
                                    .ToList()
                            })
                            .ToList()
                        : x.Cities
                            .Where(c => c.IsActive == true && c.StateId == null)
                            .Select(c => new DefaultMultiLevelDictionaryResponse
                            {
                                Id = c.Id,
                                ParentId = c.CountryId,
                                HasNextLevel = false,
                                NextLevelChildName = null,
                                Code = c.Code,
                                Name = c.Name,
                                SortIndex = c.SortIndex,
                                IsActive = c.IsActive,
                            })
                            .ToList()
                })
                .ToListAsync(cancellation)
                .ConfigureAwait(false);
            //var getCountries = await _countryRepository
            //    .GetAllAsQueryable()
            //    .Include(x => x.States.Where(s => s.IsActive == true))
            //    .ThenInclude(x => x.Cities.Where(c => c.IsActive == true))
            //    .Include(x => x.Cities.Where(c => c.IsActive == true && c.StateId == null))
            //    .Where(x => x.IsActive == true)
            //    .Select(x => new DefaultMultiLevelDictionaryResponse()
            //    {
            //        Id = x.Id,
            //        ParentId = null,
            //        Code = x.Code,
            //        Name = x.Name,
            //        HasNextLevel = true,
            //        NextLevelChildName = x.HasStates == true ? "CHILD_STATE" : "CHILD_CITY",
            //        SortIndex = x.SortIndex,
            //        IsActive = x.IsActive,

            //        Children = x.HasStates == true ?
            //             (x.States != null && x.States.Count > 0
            //                ? x.States.Select(s => new DefaultMultiLevelDictionaryResponse()
            //                {
            //                    Id = s.Id,
            //                    ParentId = s.CountryId,
            //                    Code = s.Code,
            //                    Name = s.Name,
            //                    HasNextLevel = true,
            //                    NextLevelChildName = "CHILD_CITY",
            //                    SortIndex = s.SortIndex,
            //                    IsActive = s.IsActive,
            //                    Children = s.Cities != null && s.Cities.Count > 0
            //                            ? s.Cities.Select(sc => new DefaultMultiLevelDictionaryResponse()
            //                            {
            //                                Id = sc.Id,
            //                                ParentId = sc.StateId,
            //                                HasNextLevel = false,
            //                                NextLevelChildName = null,
            //                                Code = sc.Code,
            //                                Name = sc.Name,
            //                                SortIndex = sc.SortIndex,
            //                                IsActive = sc.IsActive,
            //                            }).ToList()
            //                            : null
            //                }).ToList() : null)
            //              : (x.Cities != null && x.Cities.Count > 0 & !x.HasStates
            //                        ? x.Cities.Select(c => new DefaultMultiLevelDictionaryResponse()
            //                        {
            //                            Id = c.Id,
            //                            ParentId = c.CountryId,
            //                            Code = c.Code,
            //                            Name = c.Name,
            //                            HasNextLevel = false,
            //                            NextLevelChildName = null,
            //                            SortIndex = c.SortIndex,
            //                            IsActive = c.IsActive,
            //                        }).ToList()
            //                        : null),

            //    }).ToListAsync(cancellation).ConfigureAwait(false);

            return ServiceResult<IEnumerable<DefaultMultiLevelDictionaryResponse>>.SuccessResult(getCountries);
        }
    }
}
