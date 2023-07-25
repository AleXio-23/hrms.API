namespace hrms.Application.Services.Vacation.PayedLeaves.GetCurrentActivePayedLeaves
{
    public interface IGetCurrentActivePayedLeavesService
    {
        Task<string> Execute(int userId, CancellationToken cancellationToken);
    }
}
