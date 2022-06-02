using System;

namespace Rest.Domain.Common;

public abstract class AuditableEntity
{
    public DateTime Created { get; set; }
    public long CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public long? LastModifiedBy { get; set; }
}
