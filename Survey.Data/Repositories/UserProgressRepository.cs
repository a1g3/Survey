using System.Linq;
using Survey.Data.Infastructure;
using Survey.Data.Infastructure.Interfaces;
using Survey.Domain.Entities;
using Survey.Domain.Interfaces.Repositories;

namespace Survey.Data.Repositories
{
    public class UserProgressRepository : SurveyRepository<UserProgressEntity>, IUserProgressRepository
    {
        public UserProgressRepository(IDatabaseFactory factory) : base(factory) { }

        public int GetCurrentPartNumber(string userId)
        {
            return (from userProgress in this.Db
                    where userProgress.UserId == userId
                    select userProgress.PartNumber).Single();
        }

        public UserProgressEntity GetCurrentProgress(string userId)
        {
            return (from userProgress in this.Db
                    where userProgress.UserId == userId
                    select userProgress).Single();
        }

        public QuestionEntity GetCurrentQuestion(string userId)
        {
            return (from userProgress in this.Db
                    where userProgress.User.UserId == userId
                    select userProgress.Question).Single();
        }

        public int GetQuestionCount(string userId)
        {
            return (from userProgress in this.Db
                    where userProgress.User.UserId == userId
                    select userProgress.QuestionNumber).Single();
        }
    }
}
