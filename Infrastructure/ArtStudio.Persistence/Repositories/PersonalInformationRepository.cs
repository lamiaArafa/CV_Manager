using CV_Manager.Domain.Entities;
using CV_Manager.Domain.Interfaces.Repository;
using CV_Manager.Infrastructure.Presistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_Manager.Persistence.Repositories
{
    public class PersonalInformationRepository : GenericRepository<PersonalInformation>, IPersonalInformationRepository
    {
        public PersonalInformationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
