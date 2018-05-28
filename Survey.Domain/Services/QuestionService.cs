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
            var currentPart = UserProgressRepository.GetCurrentPartNumber(userId);

            if (currentPart == 1 && questionCount > 9) return null;
            if (currentPart == 2 && questionCount > 2) return null;

            var userInformation = UserRepository.GetUserInformation(userId);
            QuestionModel question;

            if (currentPart == 1)
            {
                if (SurveySettings.ControlQuestions.Contains(questionCount))
                    question = new QuestionModel("Two letters will appear on the screen. Your task is to examine the options and then pick one of the letters. There is no correct answer for any quesiton.", GenerateOptionsForPartOne(userInformation.Name, Enums.QuestionType.CONTROL), Enums.QuestionType.CONTROL);
                else
                    question = new QuestionModel("Two letters will appear on the screen. Your task is to examine the options and then pick one of the letters. There is no correct answer for any quesiton.", GenerateOptionsForPartOne(userInformation.Name, Enums.QuestionType.EXPERIMENTAL), Enums.QuestionType.EXPERIMENTAL);
            } else
            {
                question = GenerateOptionsForPartTwo(questionCount, userInformation.BirthDate);
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

        private QuestionModel GenerateOptionsForPartTwo(int questionCount, DateTime birthDate)
        {
            Random random = new Random();
            List<double> options = new List<double>();
            var group = random.NextDouble() >= 0.5; //true means experimental
            string question = "";
            double date = 0.0;
            string price = "";

            switch (questionCount)
            {
                case 0:
                    price = "34";
                    date = birthDate.Month;
                    question = "You and your friends are trying to decide if you want to go to dinner.  The dinner will cost {0} including a 15% gratuity and taxes. How likely would you go to this dinner?";
                    break;
                case 1:
                    price = "37";
                    date = birthDate.Day;
                    question = "You are trying to decide if you should order a new item online. After taxes and shipping, the item will cost {0}. How likely are you to buy this item?";
                    break;
                case 2:
                    price = "41";
                    date = int.Parse(birthDate.Year.ToString().Substring(1, 2));
                    question = "You are trying to decide if you want to bid on a item on TV. After taxes, the item will cost {0}. How likely are you to buy this item?";
                    break;
                default:
                    question = "ERROR!";
                    break;
            }
            date = date * 0.01;
            if (!group)
                if (date <= 0.05)
                    date += 0.05;
                else if (date >= 0.95)
                    date -= 0.05;
                else
                    if (random.NextDouble() >= 0.5)
                        date += 0.05;
                    else
                        date -= 0.05;
            double finalPrice = double.Parse(price) + date;

            return new QuestionModel(String.Format(question, finalPrice.ToString("C")), new[] { "1[Definitely Not]", "2[Probably Not]", "3[Maybe]", "4[Probably]", "5[Definitely]" }.ToList(), group ? Enums.QuestionType.EXPERIMENTAL : Enums.QuestionType.CONTROL);
        }
    }
}
