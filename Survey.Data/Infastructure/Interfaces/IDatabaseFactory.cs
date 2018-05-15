using System;

namespace Survey.Data.Infastructure.Interfaces
{
    public interface IDatabaseFactory : IDisposable
    {
        SurveyContext GetContext();
    }
}
