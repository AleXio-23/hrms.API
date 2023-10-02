using hrms.Application.Services.Vacation.PayedLeaves.GetCurrentActivePayedLeaves;
using hrms.Application.Services.Vacation.PayedLeaves.Management.GetAllPayedLeaves;
using hrms.Application.Services.Vacation.SickLeaves.Management.GetAllSickLeaves;
using hrms.Application.Services.Vacation.UnpayedLeaves.Management.GetAllUnpayedLeaves;
using hrms.Domain.Models.Vacations.GetAllLeavesForUser;
using hrms.Domain.Models.Vacations.PayedLeave;
using hrms.Domain.Models.Vacations.SickLeave;
using hrms.Domain.Models.Vacations.UnpayedLeave;
using hrms.Shared.Models;

namespace hrms.Application.Services.Vacation.GetAllLeavesForUser
{
    public class GetAllLeavesForUserService : IGetAllLeavesForUserService
    {
        private readonly IGetAllPayedLeavesService _getAllPayedLeavesService;
        private readonly IGetAllUnpayedLeavesService _getAllUnpayedLeavesService;
        private readonly IGetAllSickLeavesService _getAllSickLeavesService;

        public GetAllLeavesForUserService(IGetAllPayedLeavesService getAllPayedLeavesService, IGetAllUnpayedLeavesService getAllUnpayedLeavesService, IGetAllSickLeavesService getAllSickLeavesService)
        {
            _getAllPayedLeavesService = getAllPayedLeavesService;
            _getAllUnpayedLeavesService = getAllUnpayedLeavesService;
            _getAllSickLeavesService = getAllSickLeavesService;
        }

        public async Task<ServiceResult<GetAllLeavesResponse>> Execute(GetAllLeavesRequest request, CancellationToken cancellationToken)
        {
            List<GetAllLeavesData> RecordsData = new();
            if (request.UserId < 1)
            {
                throw new ArgumentException("WRONG_USER_ID_FORMAT");
            }

            if (request.LeaveTypes.Count == 0 || request.LeaveTypes.Contains("payed_leave"))
            {
                var filter = new GetAllPayedLeavesServiceFilter() { UserId = request.UserId };

                var payedLeaveResult = await _getAllPayedLeavesService.Execute(filter, cancellationToken).ConfigureAwait(false);
                if (payedLeaveResult?.Data?.Count > 0)
                {
                    foreach (var leave in payedLeaveResult.Data)
                    {
                        RecordsData.Add(new GetAllLeavesData()
                        {
                            LeaveId = leave.Id ?? -1,
                            LeaveType = "payed_leave",
                            DateStart = leave.DateStart,
                            DateEnd = leave.DateEnd,
                            CountDays = leave.CountDays,
                            Approved = leave.Approved,
                            ApprovedByFullname = (leave?.ApprovedByUserProfileDTO?.Firstname +
                             (!string.IsNullOrWhiteSpace(leave?.ApprovedByUserProfileDTO?.Firstname) && !string.IsNullOrWhiteSpace(leave?.ApprovedByUserProfileDTO?.Lastname) ? " " : "")
                            + leave?.ApprovedByUserProfileDTO?.Lastname)
                        });
                    }
                }
            }


            if (request.LeaveTypes.Count == 0 || request.LeaveTypes.Contains("unpayed_leave"))
            {
                var filter = new GetAllUnpayedLeavesServiceFilter() { UserId = request.UserId };

                var payedLeaveResult = await _getAllUnpayedLeavesService.Execute(filter, cancellationToken).ConfigureAwait(false);
                if (payedLeaveResult?.Data?.Count > 0)
                {
                    foreach (var leave in payedLeaveResult.Data)
                    {
                        RecordsData.Add(new GetAllLeavesData()
                        {
                            LeaveId = leave.Id ?? -1,
                            LeaveType = "unpayed_leave",
                            DateStart = leave.DateStart,
                            DateEnd = leave.DateEnd,
                            CountDays = leave.CountDays,
                            Approved = leave.Approved,
                            ApprovedByFullname = (leave?.ApprovedByUserProfileDTO?.Firstname +
                             (!string.IsNullOrWhiteSpace(leave?.ApprovedByUserProfileDTO?.Firstname) && !string.IsNullOrWhiteSpace(leave?.ApprovedByUserProfileDTO?.Lastname) ? " " : "")
                            + leave?.ApprovedByUserProfileDTO?.Lastname),
                        });
                    }
                }
            }
            if (request.LeaveTypes.Count == 0 || request.LeaveTypes.Contains("sick_leave"))
            {
                var filter = new GetAllSickLeavesServiceFilter() { UserId = request.UserId };

                var payedLeaveResult = await _getAllSickLeavesService.Execute(filter, cancellationToken).ConfigureAwait(false);
                if (payedLeaveResult?.Data?.Count > 0)
                {
                    foreach (var leave in payedLeaveResult.Data)
                    {
                        RecordsData.Add(new GetAllLeavesData()
                        {
                            LeaveId = leave.Id ?? -1,
                            LeaveType = "sick_leave",
                            DateStart = leave.DateStart,
                            DateEnd = leave.DateEnd,
                            CountDays = leave.CountDays,
                            Approved = leave.Approved,
                            ApprovedByFullname = (leave?.ApprovedByUser?.Firstname +
                             (!string.IsNullOrWhiteSpace(leave?.ApprovedByUser?.Firstname) && !string.IsNullOrWhiteSpace(leave?.ApprovedByUser?.Lastname) ? " " : "")
                            + leave?.ApprovedByUser?.Lastname),
                        });
                    }
                }
            }

            if (request.LeaveTypes.Count == 0 || request.LeaveTypes.Contains("dayoff"))
            {
                //TODO ჯერ არ არის აპი
            }

            List<GetAllLeavesData> testResult = new();
            int counter = 1;
            for (int i = 1; i < 500; i++)
            {
                foreach (var item in RecordsData)
                {
                    Random random = new();
                    bool randomValue = random.Next(1) == 0;
                    var it = new GetAllLeavesData()
                    {
                        LeaveId = item.LeaveId,
                        LeaveType = item.LeaveType,
                        DateStart = item.DateStart,
                        DateEnd = item.DateEnd,
                        CountDays = item.CountDays,
                        //Approved = item.Approved,
                        Approved = i % 2 == 0 ? null : i % 3 == 0 ? false : true,
                        ApprovedByFullname = item.ApprovedByFullname + (" - " + counter++),
                    };

                    testResult.Add(it);
                }
            }
            //var totalCount = RecordsData.Count;
            var totalCount = testResult.Count;
            //RecordsData = RecordsData.Skip(request.Offset ?? 1).Take(request.Limit ?? 30).ToList();
            testResult = testResult.Skip(request.Offset ?? 1).Take(request.Limit ?? 30).ToList();

            var finalResult = new GetAllLeavesResponse()
            {
                RecordsData = testResult,
                RecordsTotal = totalCount
            };
            return ServiceResult<GetAllLeavesResponse>.SuccessResult(finalResult);
        }
    }
}
