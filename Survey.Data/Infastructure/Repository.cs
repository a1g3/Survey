using Microsoft.EntityFrameworkCore;
using Survey.Data.Infastructure.Interfaces;

namespace Survey.Data.Infastructure
{
    public abstract class Repository<TEntity>
       where TEntity : class
    {
        protected SurveyContext DataContext { get; }
        protected DbSet<TEntity> Db => DataContext.Set<TEntity>();

        public Repository(IDatabaseFactory factory)
        {
            this.DataContext = this.GetDataContext(factory);
        }

        protected abstract SurveyContext GetDataContext(IDatabaseFactory factory);
    }
}
