using hrms.Application.Infranstructure.Interfaces.UserInterfaces;
using hrms.Infranstructure.Auth.AccessToken.UpdateAccessToken;
using hrms.Infranstructure.Auth.LogIn;
using hrms.Infranstructure.Auth.LogOut;
using hrms.Infranstructure.Auth.Register;
using hrms.Infranstructure.Auth.ResetPassword;

namespace hrms.Infranstructure.Auth
{
    public class AuthenticationFacade : IAuthenticationFacade
    {
        public IRegisterService RegisterService { get; }
        public ILogInService LoginService { get; }
        public IUpdateAccessTokenService UpdateAccessTokenService { get; }
        public ILogOutService LogOutService { get; }
        public IResetPasswordService ResetPasswordService { get; }

        public AuthenticationFacade(
            IRegisterService registerService,
            ILogInService loginService,
            IUpdateAccessTokenService updateAccessTokenService,
            ILogOutService logOutService,
            IResetPasswordService resetPasswordService)
        {
            RegisterService = registerService;
            LoginService = loginService;
            UpdateAccessTokenService = updateAccessTokenService;
            LogOutService = logOutService;
            ResetPasswordService = resetPasswordService;
        }
    }
}
