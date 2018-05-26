using System.Linq;
using Survey.Data.Infastructure;
using Survey.Data.Infastructure.Interfaces;
using Survey.Domain.Entities;
using Survey.Domain.Interfaces.Repositories;

namespace Survey.Data.Repositories
{
    public class UserRepository : SurveyRepository<UserEntity>, IUserRepository
    {
        public UserRepository(IDatabaseFactory factory) : base(factory) { }

        public UserEntity GetUserInformation(string userId)
        {
            return (from users in this.Db
                    where users.UserId == userId
                    select users).Single();
        }
    }
}
