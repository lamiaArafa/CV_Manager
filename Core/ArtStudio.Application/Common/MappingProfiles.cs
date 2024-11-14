using CV_Manager.Application.DTOs;
using AutoMapper;

namespace CV_Manager.Application.Common
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Mapping from CV to CVDto
            CreateMap<CV, CVDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.PersonalInformation.FullName))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.PersonalInformation.CityName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.PersonalInformation.Email))
                .ForMember(dest => dest.MobileNumber, opt => opt.MapFrom(src => src.PersonalInformation.MobileNumber))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.ExperienceInformation.CompanyName))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.ExperienceInformation.City))
                .ForMember(dest => dest.CompanyField, opt => opt.MapFrom(src => src.ExperienceInformation.CompanyField));

            // Mapping from CVDto to CV, including nested objects
            CreateMap<CVDto, CV>()
                .ForMember(dest => dest.PersonalInformation, opt => opt.MapFrom(src => new PersonalInformation
                {
                    FullName = src.FullName,
                    CityName = src.CityName,
                    Email = src.Email,
                    MobileNumber = src.MobileNumber
                }))
                .ForMember(dest => dest.ExperienceInformation, opt => opt.MapFrom(src => new ExperienceInformation
                {
                    CompanyName = src.CompanyName,
                    City = src.City,
                    CompanyField = src.CompanyField
                }))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PersonalInformationId, opt => opt.MapFrom(src => src.PersonalInformationId))
                .ForMember(dest => dest.ExperienceInformationId, opt => opt.MapFrom(src => src.ExperienceInformationId));
        }
    }
}
