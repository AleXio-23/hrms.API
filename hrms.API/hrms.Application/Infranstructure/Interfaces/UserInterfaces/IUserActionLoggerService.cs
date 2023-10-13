using hrms.Domain.Models.User;

namespace hrms.Application.Infranstructure.Interfaces.UserInterfaces
{
    public interface IUserActionLoggerService
    {
        Task Execute(int? userId, string? actionName, string? actionResult, string? ErrorReason, CancellationToken cancellationToken);
    }
}
