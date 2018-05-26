using System;
using System.Linq;
using Survey.Data.Infastructure;
using Survey.Data.Infastructure.Interfaces;
using Survey.Domain.Entities;
using Survey.Domain.Interfaces.Repositories;

namespace Survey.Data.Repositories
{
    public class QuestionRepository : SurveyRepository<QuestionEntity>, IQuestionRepository
    {
        public QuestionRepository(IDatabaseFactory factory) : base(factory)  { }

        public QuestionEntity GetQuestion(string questionId)
        {
            return (from questions in this.Db
                    where questions.QuestionId == questionId
                    select questions).Single();
        }

        public string GetUnansweredQuestionId(string userId)
        {
            return (from questions in this.Db
                    where questions.User.UserId == userId && String.IsNullOrEmpty(questions.Response)
                    select questions.QuestionId).Single();
        }
    }
}
