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

        private IModificationRepository<UserEntity> userRepository;

        public IModificationRepository<UserEntity> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                    userRepository = new ModificationRepository<UserEntity>(Context);
                return userRepository;
            }
        }

        public UnitOfWork(SurveyContext surveyContext)
        {
            Context = surveyContext;
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
