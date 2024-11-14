using CV_Manager.Infrastructure.Presistence;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CV_Manager.Infrastructure.Repositories;
using CV_Manager.Domain.Interfaces.Repository;

namespace CV_Manager.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(ApplicationDbContext dbContext, IMapper mapper, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        private ICVRepository? _cvRepository;
        public ICVRepository CVRepository
        {
            get
            {
                _cvRepository ??= _serviceProvider.GetRequiredService<ICVRepository>();
                return _cvRepository;
            }
        }
        private IPersonalInformationRepository? _personalInformationRepository;

        public IPersonalInformationRepository personalInformationRepository
        {
            get
            {
                _personalInformationRepository ??= _serviceProvider.GetRequiredService<IPersonalInformationRepository>();
                return _personalInformationRepository;
            }
        }
        private IExperienceInformationRepository? _experienceInformationRepository;

        public IExperienceInformationRepository experienceInformationRepository
        {
            get
            {
                _experienceInformationRepository ??= _serviceProvider.GetRequiredService<IExperienceInformationRepository>();
                return _experienceInformationRepository;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed;
        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();

        }

    }
}
