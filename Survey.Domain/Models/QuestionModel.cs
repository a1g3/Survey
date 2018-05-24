﻿using System.Collections.Generic;
using Survey.Domain.Enums;

namespace Survey.Domain.Models
{
    public class QuestionModel
    {
        public string Question { get; }
        public List<string> Options { get; }
        public string Choice { set => Choice = value; }
        public QuestionType QuestionType { get; }

        public QuestionModel(string question, List<string> options, QuestionType questionType)
        {
            this.Question = question;
            this.Options = options;
            this.QuestionType = questionType;
        }
    }
}