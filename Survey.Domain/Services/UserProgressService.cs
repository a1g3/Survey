using Survey.Domain.Interfaces.Infastructure;
using Survey.Domain.Interfaces.Repositories;
using Survey.Domain.Interfaces.Services;

namespace Survey.Domain.Services
{
    public class UserProgressService : IUserProgressService
    {
        public IUserProgressRepository UserProgressRepository { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public int GetCurrentPartNumber(string userId)
        {
            return UserProgressRepository.GetCurrentPartNumber(userId);
        }

        public int GetAnsweredQuestionCount(string userId)
        {
            return UserProgressRepository.GetQuestionCount(userId);
        }

        public void UpdatePartNumber(string userId, int newPartNumber)
        {
            var userProgress = UserProgressRepository.GetCurrentProgress(userId);
            userProgress.PartNumber = newPartNumber;
            userProgress.QuestionNumber = 0;
            userProgress.Question = null;

            UnitOfWork.UserProgressRepository.Update(userProgress);
            UnitOfWork.Commit();
        }
    }
}
