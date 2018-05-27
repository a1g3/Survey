using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Survey.Domain.Entities;
using Survey.Domain.Interfaces.Infastructure;
using Survey.Domain.Interfaces.Repositories;
using Survey.Domain.Interfaces.Services;
using Survey.Domain.Models;
using Survey.Domain.Utils;

namespace Survey.Domain.Services
{
    public class QuestionService : IQuestionService
    {
        public IQuestionRepository QuestionRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IUserProgressRepository UserProgressRepository { get; set; }
        public IUserProgressService UserProgressService { get; set; }
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

        public QuestionModel GetCurrentQuestion(string userId)
        {
            var currentQuestion = UserProgressRepository.GetCurrentQuestion(userId);
            if (currentQuestion == null) return null;
            var question = QuestionRepository.GetQuestion(currentQuestion.QuestionId);
            return Mapper.Map<QuestionModel>(question);
        }

        public void AddQuestion(string userId, QuestionModel questionModel)
        {
            var questionId = Guid.NewGuid().ToString();

            var question = new QuestionEntity() {
                Question = questionModel.Question,
                Options = String.Join(", ", questionModel.Options),
                QuestionType = (int)questionModel.QuestionType,
                QuestionId = questionId,
                UserId = userId
            };
            UnitOfWork.QuestionRepository.Insert(question);
            UnitOfWork.Commit();

            var questionCount = UserProgressService.GetAnsweredQuestionCount(userId);
            var userProgress = UserProgressRepository.GetCurrentProgress(userId);
            userProgress.Question = question;
            userProgress.QuestionNumber++;

            UnitOfWork.UserProgressRepository.Update(userProgress);
            UnitOfWork.Commit();
        }

        public QuestionModel GetNextQuestion(string userId)
        {
            var questionCount = UserProgressRepository.GetQuestionCount(userId);
            if (questionCount > 15) return null;

            var currentPart = UserProgressRepository.GetCurrentPartNumber(userId);
            var userInformation = UserRepository.GetUserInformation(userId);
            QuestionModel question;

            if (currentPart == 1)
            {
                if (SurveySettings.ControlQuestions.Contains(questionCount))
                    question = new QuestionModel("Two letters will appear on the screen. Your task is to pick one of the letters. There is no correct answer for any quesiton.", GenerateOptionsForPartOne(userInformation.Name, Enums.QuestionType.CONTROL), Enums.QuestionType.CONTROL);
                else
                    question = new QuestionModel("Two letters will appear on the screen. Your task is to pick one of the letters. There is no correct answer for any quesiton.", GenerateOptionsForPartOne(userInformation.Name, Enums.QuestionType.EXPERIMENTAL), Enums.QuestionType.EXPERIMENTAL);
            } else
            {
                if (SurveySettings.ControlQuestions.Contains(questionCount))
                    question = new QuestionModel("Four prices will appear on the screen. Your task is to pick a price that would most likely catch your eye at a store. There is no \"right\" answer for any quesiton.", GenerateOptionsForPartTwo(), Enums.QuestionType.CONTROL);
                else
                    question = new QuestionModel("Four prices will appear on the screen. Your task is to pick a price that would most likely catch your eye at a store. There is no \"right\" answer for any quesiton.", GenerateOptionsForPartTwo(userInformation.BirthDate), Enums.QuestionType.EXPERIMENTAL);
            }

            AddQuestion(userId, question);

            return question;
        }

        private List<string> GenerateOptionsForPartOne(string name, Enums.QuestionType questionType)
        {
            Random random = new Random();
            List<string> options = new List<string>();
            List<string> alphabet = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            List<string> nameCharacters = name.ToUpper().ToCharArray().Select(x => x.ToString()).ToList();

            if (questionType == Enums.QuestionType.EXPERIMENTAL)
            {
                var index = random.Next(0, name.Length - 1);
                options.Add(name[index].ToString());
            } else
            {
                var nonNameOptions = alphabet.Except(nameCharacters);
                var index = random.Next(0, alphabet.Count);
                options.Add(alphabet[index]);
            }
            var notUsedLetters = alphabet.Except(nameCharacters).Except(options).ToList();
            var randomIndex = random.Next(0, notUsedLetters.Count() - 1);
            options.Add(notUsedLetters[randomIndex]);

            return options.Shuffle();
        }

        private List<string> GenerateOptionsForPartTwo(DateTime? birthDate = null)
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
                options.Add(random.NextDouble() * 100);
            
            for (int x = 0; x != 3; x++)
            {
                var offset = random.NextDouble() * 10;

                if (random.NextDouble() >= 0.5)
                    options.Add(options.First() + offset);
                else
                    //Make sure the value is not negative
                    options.Add(Math.Abs(options.First() - offset));
            }

            return options.Select(x => x.ToString("C")).ToList().Shuffle();
        }
    }
}
