using hrms.Domain.Models.Shared;

namespace hrms.Application.Infranstructure.Interfaces.UserInterfaces
{
    public interface IUserAgentAndIpAddressService
    {
        UserAgentAndIpAddressResponse Execute();
    }
}
