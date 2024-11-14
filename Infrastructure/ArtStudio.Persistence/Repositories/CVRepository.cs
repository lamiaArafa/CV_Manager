using CV_Manager.Domain.Entities;
using CV_Manager.Domain.Interfaces.Repository;
using CV_Manager.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace CV_Manager.Persistence.Repositories
{
    public class CVRepository : GenericRepository<CV>, ICVRepository
    {
        public CVRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<CV?> GetCVById(int id)
        {

            return await GetDbSet().Include(w => w.PersonalInformation).Include(w => w.ExperienceInformation)
            .FirstOrDefaultAsync(d=> d.Id == id);

        }
        public async Task<List<CV>> GetAllCVAsync()
        {

            return await GetDbSet().Include(w => w.PersonalInformation).Include(w => w.ExperienceInformation)
            .ToListAsync();

        }
    }
}
