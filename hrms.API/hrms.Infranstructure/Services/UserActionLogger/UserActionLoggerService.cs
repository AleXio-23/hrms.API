using hrms.Application.Infranstructure.Interfaces.UserInterfaces;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;

namespace hrms.Infranstructure.Services.UserActionLogger
{
    public class UserActionLoggerService : IUserActionLoggerService
    {
        private readonly IRepository<UserAuthLog> _userAuthLogsRepository;
        private readonly IUserAgentAndIpAddressService _userAgentAndIpAddressService;

        public UserActionLoggerService(IRepository<UserAuthLog> userAuthLogsRepository, IUserAgentAndIpAddressService userAgentAndIpAddressService)
        {
            _userAuthLogsRepository = userAuthLogsRepository;
            _userAgentAndIpAddressService = userAgentAndIpAddressService;
        }

        public async Task Execute(int? userId, string? actionName, string? actionResult, string? ErrorReason, CancellationToken cancellationToken)
        {
            var getUserAgentsInfo = _userAgentAndIpAddressService.Execute();

            var userAuthLog = new UserAuthLog()
            {
                UserId = userId,
                Ip = getUserAgentsInfo.IpAddress,
                UserAgent = getUserAgentsInfo.UserAgent,
                ActionType = actionName,
                ActionResult = actionResult,
                ErrorReason = ErrorReason
            };

            await _userAuthLogsRepository.Add(userAuthLog, cancellationToken);
        }
    }
}
