using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class UserUploadedDocument
{
    public int Id { get; set; }

    public int? UploadedByUserId { get; set; }

    public DateTime UploadDate { get; set; }

    public int? DocumentTypeId { get; set; }

    public string? DocumentTypeIfNotFoundInDicitonary { get; set; }

    public string? DocumentName { get; set; }

    public virtual DocumentType? DocumentType { get; set; }

    public virtual ICollection<SickLeaf> SickLeaves { get; set; } = new List<SickLeaf>();

    public virtual User? UploadedByUser { get; set; }
}
