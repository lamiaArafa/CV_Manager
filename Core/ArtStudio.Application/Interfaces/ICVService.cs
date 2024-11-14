
using CV_Manager.Application.DTOs;

namespace CV_Manager.Application.Interfaces;

public interface ICVService
{
    public  Task<CV> CreateORUpdateCV(CVDto cvDto);
    public  Task<CV> DeleteCVAsync(int id);
    public  Task<List<CVDto>> GetAllCVAsync();
    public Task<CVDto> GetCVByIdAsync(int id);
}
