using hrms.Application.Infranstructure.Interfaces.UserInterfaces;
using Microsoft.AspNetCore.Http;

namespace hrms.Infranstructure.Services.CurrentUserId
{
    public class GetCurrentUserIdService : IGetCurrentUserIdService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetCurrentUserIdService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int Execute()
        {
            string? userId = _httpContextAccessor.HttpContext.User.FindFirst("Id")?.Value ?? throw new UnauthorizedAccessException("Authorised user id not found");

            var tryUserIdParse = Int32.TryParse(userId, out var tryUserId);
            if (!tryUserIdParse)
            {
                throw new UnauthorizedAccessException("Wrong user id format");
            }

            return tryUserId;
        }
    }
}

