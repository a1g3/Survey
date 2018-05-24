using System;
using Survey.Domain.Interfaces.Repositories;
using Survey.Domain.Interfaces.Services;

namespace Survey.Domain.Services
{
    public class QuestionService : IQuestionService
    {
        public IQuestionRepository QuestionRepository { get; set; }

        public void AddAnswer(Guid userId, string answer)
        {
            throw new NotImplementedException();
        }
    }
}
