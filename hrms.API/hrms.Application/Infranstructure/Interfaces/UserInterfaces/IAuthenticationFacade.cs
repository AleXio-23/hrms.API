namespace hrms.Application.Infranstructure.Interfaces.UserInterfaces
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
