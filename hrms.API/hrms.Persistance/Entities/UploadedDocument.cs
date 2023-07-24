using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class UploadedDocument
{
    public int Id { get; set; }

    public DateTime UploadDate { get; set; }

    public string? DocumentBase64String { get; set; }

    public string? DocumentName { get; set; }

    public bool? IsActive { get; set; }
}
