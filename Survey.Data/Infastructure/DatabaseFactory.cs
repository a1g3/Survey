using System;
using Survey.Data.Infastructure.Interfaces;

namespace Survey.Data.Infastructure
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private bool IsDisposed { get; set; }
        private Lazy<SurveyContext> mSurveyContext;

        public DatabaseFactory()
        {
            mSurveyContext = new Lazy<SurveyContext>();
        }

        public DatabaseFactory(SurveyContext surveyContext)
        {
            mSurveyContext = new Lazy<SurveyContext>(surveyContext);
        }

        public SurveyContext GetContext()
        {
            return mSurveyContext.Value;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposed)
        {
            if (IsDisposed)
            {
                if (disposed && mSurveyContext != null && mSurveyContext.IsValueCreated)
                    mSurveyContext.Value.Dispose();
                mSurveyContext = null;
                IsDisposed = true;
            }
        }
        ~DatabaseFactory()
        {
            Dispose(false);
        }
    }
}
