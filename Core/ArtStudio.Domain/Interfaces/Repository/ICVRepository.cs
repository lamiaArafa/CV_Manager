
using CV_Manager.Domain.Entities;


namespace CV_Manager.Domain.Interfaces.Repository;

public interface ICVRepository : IGenericRepository<CV>
{
    Task<List<CV>> GetAllCVAsync();
    Task<CV?> GetCVById(int id);

}
