using System.Collections.Generic;

namespace Survey.Domain.Interfaces.Infastructure
{
    public interface ISurveySettings
    {
        List<int> ControlQuestions { get; }
    }
}
