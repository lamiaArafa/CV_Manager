using CV_Manager.Domain.CommonEntities;


namespace CV_Manager.Domain.Entities;

public class PersonalInformation:BaseEntity 
{
    public string? FullName { get; set; }
    public string? CityName { get; set; }
    public string? Email { get; set; }
    public string? MobileNumber { get; set; }
}
