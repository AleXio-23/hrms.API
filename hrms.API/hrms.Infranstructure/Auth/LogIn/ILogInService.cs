using hrms.Domain.Models.Auth;
using hrms.Shared.Models;
using System;

namespace hrms.Infranstructure.Auth.LogIn
{
    public interface ILogInService
    {
        Task<ServiceResult<LoginResponse>> Exeute(LoginDto loginDto, CancellationToken cancellationToken);
    }
}
