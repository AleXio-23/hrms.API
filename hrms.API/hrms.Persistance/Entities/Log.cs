using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class Log
{
    public long Id { get; set; }

    public DateTime LogDate { get; set; }

    public string LogLevel { get; set; } = null!;

    public string MethodName { get; set; } = null!;

    public string LogMessage { get; set; } = null!;

    public string? ExceptionMessage { get; set; }
}
