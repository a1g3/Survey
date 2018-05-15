using Survey.Domain.Entities;

namespace Survey.Domain.Interfaces.Infastructure
{
    public interface IUnitOfWork
    {
        IModificationRepository<UserEntity> UserEntity { get; }
        void Commit();
    }
}
