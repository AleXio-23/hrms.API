using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class SickLeaf
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }

    public int CountDays { get; set; }

    public int? DocumentId { get; set; }

    public bool? Approved { get; set; }

    public int? ApprovedByUserId { get; set; }

    public string? Comment { get; set; }

    public virtual User? ApprovedByUser { get; set; }

    public virtual UserUploadedDocument? Document { get; set; }

    public virtual User User { get; set; } = null!;
}
