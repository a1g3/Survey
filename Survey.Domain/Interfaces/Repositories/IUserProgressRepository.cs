using Survey.Domain.Entities;

namespace Survey.Domain.Interfaces.Repositories
{
    public interface IUserProgressRepository
    {
        QuestionEntity GetCurrentQuestion(string userId);
        int GetQuestionCount(string userId);
    }
}
