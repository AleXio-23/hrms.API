﻿using System.Diagnostics;

namespace hrms.Application.Infranstructure.Interfaces
{
    public interface ILogger
    {
        Task LogInformationAsync(string methodName, string message, CancellationToken cancellationToken);
        Task LogError(string methodName, string message, CancellationToken cancellationToken, Exception? exception = null);
        Task LogWarningAsync(string methodName, string message, CancellationToken cancellationToken);
    }
}
