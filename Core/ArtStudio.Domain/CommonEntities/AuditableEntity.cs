

namespace CV_Manager.Domain.CommonEntities;

public abstract class AuditableEntity : BaseEntity
{
    public int? CreatedBy { get; set; }
    public int? ModiefiedBy { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? ModiefiedAt { get; set; }
}
