using hrms.Infranstructure.Auth.AccessToken.UpdateAccessToken;
using hrms.Infranstructure.Auth.LogIn;
using hrms.Infranstructure.Auth.LogOut;
using hrms.Infranstructure.Auth.Register;
using hrms.Infranstructure.Auth.ResetPassword;

namespace hrms.Infranstructure.Auth
{
    public interface IAuthenticationFacade
    {
        IRegisterService RegisterService { get; }
        ILogInService LoginService { get; }
        IUpdateAccessTokenService UpdateAccessTokenService { get; }
        ILogOutService LogOutService { get; }
        IResetPasswordService ResetPasswordService { get; }
    }
}
