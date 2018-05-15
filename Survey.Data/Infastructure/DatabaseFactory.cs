using System;
using Survey.Data.Infastructure.Interfaces;

namespace Survey.Data.Infastructure
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private bool IsDisposed { get; set; }
        private Lazy<SurveyContext> mVulnerabilitiesContext;

        public DatabaseFactory()
        {
            mVulnerabilitiesContext = new Lazy<SurveyContext>();
        }

        public DatabaseFactory(SurveyContext vulnerabilitiesContext)
        {
            mVulnerabilitiesContext = new Lazy<SurveyContext>(vulnerabilitiesContext);
        }

        public SurveyContext GetContext()
        {
            return mVulnerabilitiesContext.Value;
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
                if (disposed && mVulnerabilitiesContext != null && mVulnerabilitiesContext.IsValueCreated)
                    mVulnerabilitiesContext.Value.Dispose();
                mVulnerabilitiesContext = null;
                IsDisposed = true;
            }
        }
        ~DatabaseFactory()
        {
            Dispose(false);
        }
    }
}
