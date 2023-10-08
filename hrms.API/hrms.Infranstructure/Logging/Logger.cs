using hrms.Application.Infranstructure.Interfaces;
using hrms.Persistance;
using hrms.Persistance.Entities;

namespace hrms.Infranstructure.Logging
{
    public class Logger : ILogger
    {
        private readonly HrmsAppDbContext _dbContext;

        public Logger(HrmsAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task LogError(string methodName, string message, CancellationToken cancellationToken, Exception? exception = null)
        {
            var newLog = new Log()
            {
                LogLevel = "error",
                MethodName = methodName,
                LogMessage = message,
                ExceptionMessage = exception != null ? exception?.ToString() : null
            };
            _dbContext.Logs.Add(newLog);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task LogInformationAsync(string methodName, string message, CancellationToken cancellationToken)
        {
            var newLog = new Log()
            {
                LogLevel = "info",
                MethodName = methodName,
                LogMessage = message
            };
            _dbContext.Logs.Add(newLog);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task LogWarningAsync(string methodName, string message, CancellationToken cancellationToken)
        {
            var newLog = new Log()
            {
                LogLevel = "info",
                MethodName = methodName,
                LogMessage = message
            };
            _dbContext.Logs.Add(newLog);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
