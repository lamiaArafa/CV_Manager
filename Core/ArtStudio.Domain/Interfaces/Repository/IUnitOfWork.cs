using CV_Manager.Domain.Interfaces.Repository;


namespace CV_Manager.Infrastructure.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICVRepository CVRepository { get; }
        IPersonalInformationRepository personalInformationRepository { get; }
        IExperienceInformationRepository experienceInformationRepository { get; }
        Task<int> SaveAsync();

    }
}
