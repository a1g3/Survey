using System;
using Survey.Domain.Entities;
using Survey.Domain.Interfaces.Infastructure;

namespace Survey.Data.Infastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private SurveyContext Context { get; }

        public UnitOfWork()
        {
            Context = new SurveyContext();
        }

        private IModificationRepository<UserEntity> cveRepository;

        public IModificationRepository<UserEntity> UserEntity
        {
            get
            {
                if (this.cveRepository == null)
                    cveRepository = new ModificationRepository<UserEntity>(Context);
                return cveRepository;
            }
        }

        public UnitOfWork(SurveyContext vulnerabilitiesContext)
        {
            Context = vulnerabilitiesContext;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            this.Commit();
        }
    }
}
