using hrms.Domain.Models.Auth;
using hrms.Shared.Models;
using System;

namespace hrms.Application.Infranstructure.Interfaces.UserInterfaces
{
    public interface ILogInService
    {
        Task<ServiceResult<LoginResponse>> Exeute(LoginDto loginDto, CancellationToken cancellationToken);
    }
}
