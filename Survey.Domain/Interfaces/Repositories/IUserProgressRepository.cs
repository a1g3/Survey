using Survey.Domain.Entities;

namespace Survey.Domain.Interfaces.Repositories
{
    public interface IUserProgressRepository
    {
        UserProgressEntity GetCurrentProgress(string userId);
        QuestionEntity GetCurrentQuestion(string userId);
        int GetQuestionCount(string userId);
        int GetCurrentPartNumber(string userId);
    }
}
