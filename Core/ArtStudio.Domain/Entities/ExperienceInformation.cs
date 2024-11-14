

using CV_Manager.Domain.CommonEntities;

namespace CV_Manager.Domain.Entities;

public class ExperienceInformation:BaseEntity
{
    public string? CompanyName { get; set; }
    public string? City { get; set; }
    public string? CompanyField { get; set; }
}
