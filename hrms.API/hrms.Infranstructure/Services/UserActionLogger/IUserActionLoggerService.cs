using hrms.Domain.Models.User;

namespace hrms.Infranstructure.Services.UserActionLogger
{
    public interface IUserActionLoggerService
    {
        Task Execute(int? userId, string? actionName, string? actionResult, string? ErrorReason, CancellationToken cancellationToken);
    }
}
