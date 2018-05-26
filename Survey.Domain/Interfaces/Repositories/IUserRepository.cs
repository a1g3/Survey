using Survey.Domain.Entities;

namespace Survey.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        UserEntity GetUserInformation(string userId);
    }
}
