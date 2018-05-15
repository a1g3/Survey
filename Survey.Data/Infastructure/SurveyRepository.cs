using Survey.Data.Infastructure.Interfaces;

namespace Survey.Data.Infastructure
{
    public abstract class SurveyRepository<TEntity> : Repository<TEntity> where TEntity : class
    {
        public SurveyRepository(IDatabaseFactory databaseFactory) : base(databaseFactory) { }

        protected override SurveyContext GetDataContext(IDatabaseFactory factory)
        {
            return factory.GetContext();
        }
    }
}
