﻿using hrms.Domain.Models.Auth;
using hrms.Persistance.Entities;
using hrms.Shared.Models;

namespace hrms.Infranstructure.Auth
{
    public interface IAuthService
    {
        Task<ServiceResult<User>> Register(RegisterDto registerDto, CancellationToken cancellationToken);
        Task<ServiceResult<string>> Login(LoginDto loginDto, CancellationToken cancellationToken);
        Task<ServiceResult<string>> UpdateAccessToken(string accessToken, CancellationToken cancellationToken);
        Task<ServiceResult<string>> ResetPassword(string usernameOrEmail, CancellationToken cancellationToken);
    }
}
