
using AutoMapper;
using CV_Manager.Application.Common.Exceptions;
using CV_Manager.Application.DTOs;
using CV_Manager.Application.Interfaces;
using CV_Manager.Domain.Interfaces.Repository;

namespace CV_Manager.Application.Services;
public class CVService : ICVService
{
    private readonly IMapper mapper;

    private readonly ICVRepository cvRepository;
    private readonly IPersonalInformationRepository personalInformationRepository;
    private readonly IExperienceInformationRepository experienceInformationRepository;
    public CVService(ICVRepository _cvRepository, IPersonalInformationRepository _personalInformationRepository,
    IExperienceInformationRepository _experienceInformationRepository, IMapper _mapper)
    {
        experienceInformationRepository = _experienceInformationRepository;
        personalInformationRepository = _personalInformationRepository;
        cvRepository = _cvRepository;
        mapper = _mapper;
    }
    
    private async Task CheckCVDataValidation(CVDto cvDto)
    {
        var isMobileExist = await cvRepository.AnyAsync(c => c.Id != cvDto.Id && c.PersonalInformation.MobileNumber == cvDto.MobileNumber);

        if (isMobileExist)
            throw new DuplicatedMobileException();

        var isNameExist = await cvRepository.AnyAsync(c => c.Id != cvDto.Id && c.Name == cvDto.Name);
        if (isNameExist)
            throw new DuplicatedNameException();
    }
    private async Task<CV?> GetOldCVAsync(int cvId)
    {
        var oldCV = await cvRepository.GetCVById(cvId);
        return oldCV;
    }
    private async Task<CV> UpdateCV(CVDto cvDto)
    {
        CV oldCV = await GetOldCVAsync(cvDto.Id.Value);
        oldCV.Name = cvDto.Name;
        oldCV.PersonalInformation.FullName = cvDto.FullName;
        oldCV.ExperienceInformation.CompanyName = cvDto.CompanyName;


        cvRepository.Update(oldCV);

        return oldCV;
    }
    public async Task<CV> CreateCVObj(CVDto cvDto)
    {
        var cv = mapper.Map<CV>(cvDto);
        await cvRepository.AddAsync(cv);

        return cv;
    }

    public async Task<CV> CreateORUpdateCV(CVDto cvDto)
    {
        var validationResult = cvDto.Validate();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.ErrorMessage);

        await CheckCVDataValidation(cvDto);

        if (cvDto.Id > 0)
        {
            return await UpdateCV(cvDto);
        }

        return await CreateCVObj(cvDto);
    }

    private async Task<PersonalInformation> GetOldPersonalInformationAsync(int? personalInformationId)
    {
        var oldPersonalInformation = await personalInformationRepository.FindAsync(personalInformationId.Value);
        return oldPersonalInformation;
    }
    private async Task<ExperienceInformation> GetOldExperienceInformationAsync(int? experienceInformation)
    {
        var oldExperienceInformation = await experienceInformationRepository.FindAsync(experienceInformation.Value);
        return oldExperienceInformation;
    }

    private async Task DeletePersonalInformationAsync(int? personalInformationId) 
    {
        PersonalInformation oldPersonalInformation = await GetOldPersonalInformationAsync(personalInformationId);
        await personalInformationRepository.DeleteAsync(oldPersonalInformation);

    }

    private async Task DeleteExperienceInformationAsync(int? experienceInformation)
    {
        ExperienceInformation oldExperienceInformation = await GetOldExperienceInformationAsync(experienceInformation);
        await experienceInformationRepository.DeleteAsync(oldExperienceInformation);

    }
    public  async Task<CV> DeleteCVAsync(int id)
    {
        CV oldCV = await  cvRepository.FindAsync(id);
        await cvRepository.DeleteAsync(oldCV);

        await DeletePersonalInformationAsync(oldCV.PersonalInformationId);
        await DeleteExperienceInformationAsync(oldCV.ExperienceInformationId);
        return oldCV;
    }

    public async Task<CVDto> GetCVByIdAsync(int id)
    {
        var oldCV = await GetOldCVAsync(id);
        var cvDto = mapper.Map<CVDto>(oldCV);
        return cvDto;
    }

    public async Task<List<CVDto>> GetAllCVAsync()
    {
        var cvs =await cvRepository.GetAllCVAsync();

        var cvDto = mapper.Map<List<CVDto>>(cvs);
        return cvDto;

    }

}