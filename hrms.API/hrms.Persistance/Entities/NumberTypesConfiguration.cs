using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class NumberTypesConfiguration
{
    public int Id { get; set; }

    public string ConfigName { get; set; } = null!;

    public int Value { get; set; }
}
