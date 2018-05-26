using System.Collections.Generic;
using Survey.Domain.Enums;

namespace Survey.Domain.Models
{
    public class QuestionModel
    {
        public string Instructions { get; set; }
        public string Question { get; }
        public List<string> Options { get; }
        public QuestionType QuestionType { get; }

        public QuestionModel(string question, List<string> options, QuestionType questionType)
        {
            this.Question = question;
            this.Options = options;
            this.QuestionType = questionType;
        }
    }
}
