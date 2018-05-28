using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Survey.Domain.Interfaces.Infastructure;
using Survey.Domain.Interfaces.Services;
using Survey.Models;

namespace Survey.Controllers
{
    public class QuestionController : Controller
    {
        public IQuestionService QuestionService { get; set; }
        public IUserProgressService UserProgressService { get; set; }
        public ISurveySettings SurveySettings { get; set; }

        [HttpGet]
        public IActionResult Part(string userId)
        {
            var questionCount = UserProgressService.GetAnsweredQuestionCount(userId);
            var currentPart = UserProgressService.GetCurrentPartNumber(userId);

            var viewModel = new UserViewModel() { UserId = userId };
            if(currentPart == 0)
            {
                viewModel.Part = 1;
                viewModel.Instructions = "Two letters will appear on the screen. Your task is to examine the options and then pick one of the letters. There is no correct answer for any quesiton.";
                UserProgressService.UpdatePartNumber(userId, 1);
            } else if(questionCount >= 9 && currentPart == 1)
            {
                viewModel.Part = 2;
                viewModel.Instructions = "A scenario will be shown on a screen along with a price. Your task is to examine the price and decide how likely you are to spend money in that scenario. There is no correct answer for any quesiton.";
                UserProgressService.UpdatePartNumber(userId, 2);
            } else if(questionCount >= 2 && currentPart == 2)
            {
                return View("SurveyComplete");
            }
                return View("PartIntro", viewModel);
        }

        [HttpGet]
        public IActionResult Question(string userId)
        {
            var currentQuestion = QuestionService.GetCurrentQuestion(userId);
            QuestionViewModel question;
            if (currentQuestion != null && String.IsNullOrEmpty(currentQuestion.Response))
                question = Mapper.Map<QuestionViewModel>(currentQuestion);
            else
            {
                var nextQuesiton = QuestionService.GetNextQuestion(userId);
                question = Mapper.Map<QuestionViewModel>(nextQuesiton);
            }
            
            if(question != null)
            {
                question.userId = userId;

                return View("Question", question);
            } else
                return RedirectToAction("Part", new { userId });
        }

        [HttpPost]
        public IActionResult Submit(string userId, string response)
        {
            QuestionService.AddAnswer(userId, response);
            return RedirectToAction("Question", new { userId });
        }
    }
}