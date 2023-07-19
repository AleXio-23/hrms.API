using hrms.Domain.Models.Shared;
using Microsoft.AspNetCore.Http;

namespace hrms.Infranstructure.Services.UserAgentAndIpAddress
{
    public class UserAgentAndIpAddressService : IUserAgentAndIpAddressService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAgentAndIpAddressService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        } 

        public UserAgentAndIpAddressResponse Execute()
        {
            var useAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();

            return new UserAgentAndIpAddressResponse()
            {
                IpAddress = ip,
                UserAgent = useAgent
            };
        }
    }
}
