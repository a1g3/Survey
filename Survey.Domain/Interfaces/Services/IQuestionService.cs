using System;

namespace Survey.Domain.Interfaces.Services
{
    public interface IQuestionService
    {
        void AddAnswer(Guid userId, string answer);
    }
}
