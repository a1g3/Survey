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

        private IModificationRepository<UserProgressEntity> userProgressRepository;

        public IModificationRepository<UserProgressEntity> UserProgressRepository
        {
            get
            {
                if (this.userProgressRepository == null)
                    userProgressRepository = new ModificationRepository<UserProgressEntity>(Context);
                return userProgressRepository;
            }
        }

        private IModificationRepository<QuestionEntity> questionRepository;

        public IModificationRepository<QuestionEntity> QuestionRepository
        {
            get
            {
                if (this.questionRepository == null)
                    questionRepository = new ModificationRepository<QuestionEntity>(Context);
                return questionRepository;
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
