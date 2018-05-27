using System.Collections.Generic;
using Survey.Domain.Enums;

namespace Survey.Domain.Models
{
    public class QuestionModel
    {
        public string Question { get; }
        public List<string> Options { get; }
        public QuestionType QuestionType { get; }
        public string Response { get; set; }

        public QuestionModel(string question, List<string> options, QuestionType questionType, string response = null)
        {
            this.Question = question;
            this.Options = options;
            this.QuestionType = questionType;
            this.Response = response;
        }
    }
}
