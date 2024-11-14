

using CV_Manager.Application.DTOs.Validations;
using System.Text;
namespace CV_Manager.Application.DTOs;

public class CVDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? FullName { get; set; }
    public string? CityName { get; set; }
    public string? Email { get; set; }
    public string? MobileNumber { get; set; }
    public string? CompanyName { get; set; }
    public string? City { get; set; }
    public string? CompanyField { get; set; }
    public int? PersonalInformationId { get; set; }
    public int? ExperienceInformationId { get; set; }
    public ValidationResult Validate()
    {
        var errors = new StringBuilder();
        if (string.IsNullOrEmpty(Name) || Name.Trim().Length < 4 || Name.Length > 200)
            errors.Append("Name should be greater than 3 char and less than 200 ");
        if (string.IsNullOrWhiteSpace(MobileNumber) || MobileNumber.Length < 6 || MobileNumber.Length > 15)
            errors.Append("Mobile number should be greater than 5 char and less than 15 ");
        if(!MobileNumber!.All(char.IsDigit))
            errors.Append("Mobile number should be only digits ");

            if (!string.IsNullOrWhiteSpace(Email) && !Email.IsValidEmail())
            errors.Append("Invalid Email Address");

        
        return new ValidationResult { IsValid = errors.Length == 0, ErrorMessage = errors.ToString() };
    }
}
