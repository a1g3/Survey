using System;
using System.Collections.Generic;
using System.Linq;
using Survey.Domain.Interfaces.Infastructure;
using Survey.Domain.Interfaces.Repositories;
using Survey.Domain.Interfaces.Services;
using Survey.Domain.Models;

namespace Survey.Domain.Services
{
    public class QuestionService : IQuestionService
    {
        public IQuestionRepository QuestionRepository { get; set; }
        public IUserProgressRepository UserProgressRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public ISurveySettings SurveySettings { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public void AddAnswer(string userId, string answer)
        {
            var currentQuestion = UserProgressRepository.GetCurrentQuestion(userId);
            var question = QuestionRepository.GetQuestion(currentQuestion.QuestionId);

            question.Response = answer;
            UnitOfWork.QuestionRepository.Update(question);
            UnitOfWork.Commit();
        }

        public int GetAnsweredQuestionCount(string userId)
        {
            return UserProgressRepository.GetQuestionCount(userId);
        }

        public QuestionModel GetNextQuestion(string userId)
        {
            var questionCount = UserProgressRepository.GetQuestionCount(userId);
            if (questionCount > 15) return null;

            var userInformation = UserRepository.GetUserInformation(userId);
            QuestionModel question;

            if (SurveySettings.ControlQuestions.Contains(questionCount))
                question = new QuestionModel("What would you pay for a new bike?", GenerateOptions(), Enums.QuestionType.CONTROL);
            else
                question = new QuestionModel("What would you pay for a new bike?", GenerateOptions(userInformation.BirthDate), Enums.QuestionType.EXPERIMENTAL);

            //TODO: Add question to Db

            return question;
        }

        private List<string> GenerateOptions(DateTime? birthDate = null)
        {
            Random random = new Random();
            List<double> options = new List<double>();
            if (birthDate.HasValue)
            {
                string birthDateString = birthDate.Value.Month.ToString() + birthDate.Value.Day.ToString() + birthDate.Value.Year.ToString();
                string birthDatePrice = "";

                for (var x = 0; x != 4; x++)
                    birthDatePrice += birthDateString[random.Next(0, birthDateString.Length - 1)].ToString();
                birthDatePrice = birthDatePrice.Insert(2, ".");
                options.Add(double.Parse(birthDatePrice));
            } else
                options.Add(Math.Round(random.NextDouble() * 100, 2));
            
            for (int x = 0; x != 3; x++)
            {
                var offset = Math.Round(random.NextDouble(), 2);

                if (random.NextDouble() >= 0.5)
                    options.Add(options.First() + offset);
                else
                    options.Add(options.First() - offset);
            }

            return options.Select(x => x.ToString()).ToList();
        }
    }
}
