using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Survey.Domain.Entities;
using Survey.Domain.Models;

namespace Survey.Domain.Utils
{
    public class QuestionEntityToModel : ITypeConverter<QuestionEntity, QuestionModel>
    {
        public QuestionModel Convert(QuestionEntity source, QuestionModel destination, ResolutionContext context)
        {
            var options = source.Options.Split(", ").ToList();
            destination = new QuestionModel(source.Question, options, (Enums.QuestionType)source.QuestionType, source.Response);
            return destination;
        }
    }
}
