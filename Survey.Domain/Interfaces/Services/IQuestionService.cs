using System;

namespace Survey.Domain.Interfaces.Services
{
    public interface IQuestionService
    {
        void InsertQuestion();
        void AddAnswer(Guid questionId, string answer);
    }
}
