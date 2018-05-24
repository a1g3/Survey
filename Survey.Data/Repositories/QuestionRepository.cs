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

        public int GetAnsweredQuestionCount(string userId)
        {
            return this.DataContext.Questions.Where(x => x.User.UserId == userId).Count();
        }
    }
}
