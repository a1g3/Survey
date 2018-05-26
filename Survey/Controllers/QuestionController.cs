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
                viewModel.Instructions = "Two letters will appear on the screen. Your task is to pick one of the letters. There is no \"right\" answer for any quesiton.";
                UserProgressService.UpdatePartNumber(userId, 1);
            } else if(questionCount >= 15 && currentPart == 1)
            {
                viewModel.Part = 2;
                viewModel.Instructions = "Four prices will appear on the screen. Your task is to pick a price that would most likely catch your eye at a store. There is no \"right\" answer for any quesiton.";
                UserProgressService.UpdatePartNumber(userId, 2);
            }
                return View("PartIntro", viewModel);
        }

        [HttpGet]
        public IActionResult Question(string userId)
        {
            var nextQuesiton = QuestionService.GetNextQuestion(userId);
            if(nextQuesiton != null)
            {
                var question = Mapper.Map<QuestionViewModel>(nextQuesiton);
                question.userId = userId;

                return View("Question", question);
            } else
            {
                return RedirectToAction("Part", new { userId });
            }
        }

        [HttpPost]
        public IActionResult Submit(string userId, string Response)
        {
            QuestionService.AddAnswer(userId, Response);
            return RedirectToAction("Question", new { userId });
        }
    }
}