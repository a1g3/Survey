using Survey.Data.Infastructure;
using Survey.Data.Infastructure.Interfaces;
using Survey.Domain.Entities;

namespace Survey.Data.Repositories
{
    public class QuestionRepository : SurveyRepository<QuestionEntity>
    {
        public QuestionRepository(IDatabaseFactory factory) : base(factory) { }
    }
}
