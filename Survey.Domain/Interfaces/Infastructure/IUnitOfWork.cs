using Survey.Domain.Entities;

namespace Survey.Domain.Interfaces.Infastructure
{
    public interface IUnitOfWork
    {
        IModificationRepository<UserEntity> UserRepository { get; }
        IModificationRepository<QuestionEntity> QuestionRepository { get; }
        IModificationRepository<UserProgressEntity> UserProgressRepository { get; }
        void Commit();
    }
}
