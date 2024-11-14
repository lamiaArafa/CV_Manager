
using CV_Manager.Domain.CommonEntities;

namespace CV_Manager.Domain.Entities;

public class CV:AuditableEntity
{
    public string? Name { get; set; }
    public int PersonalInformationId { get; set; }
    public int ExperienceInformationId { get; set; }
    public PersonalInformation? PersonalInformation { get; set; }
    public ExperienceInformation? ExperienceInformation { get; set; }
}
