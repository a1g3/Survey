using System.Collections.Generic;
using Survey.Domain.Interfaces.Infastructure;

namespace Survey.Domain.Infastructure
{
    public class SurveySettings : ISurveySettings
    {
        public List<int> ControlQuestions { get; set; }

        public SurveySettings(List<int> controlQuestions)
        {
            ControlQuestions = controlQuestions;
        }
    }
}
