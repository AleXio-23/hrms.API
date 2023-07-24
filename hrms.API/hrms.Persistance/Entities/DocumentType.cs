using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class DocumentType
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsDocumentSizeLimited { get; set; }

    public int MaxDocumentSizeInMbsToUpload { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<UserUploadedDocument> UserUploadedDocuments { get; set; } = new List<UserUploadedDocument>();
}
