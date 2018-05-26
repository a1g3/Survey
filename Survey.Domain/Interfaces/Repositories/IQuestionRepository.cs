using Survey.Domain.Entities;

namespace Survey.Domain.Interfaces.Repositories
{
    public interface IQuestionRepository
    {
        string GetUnansweredQuestionId(string userId);
        QuestionEntity GetQuestion(string questionId);
    }
}
