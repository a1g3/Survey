using System;
using Survey.Domain.Models;

namespace Survey.Domain.Interfaces.Services
{
    public interface IQuestionService
    {
        QuestionModel GetNextQuestion(string userId);
        void AddAnswer(string userId, string answer);
    }
}
