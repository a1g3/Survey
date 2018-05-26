using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Survey.Domain.Interfaces.Services;
using Survey.Models;

namespace Survey.Controllers
{
    public class QuestionController : Controller
    {
        public IQuestionService QuestionService { get; set; }

        [HttpGet]
        public IActionResult Part(string userId)
        {
            var viewModel = new UserViewModel() { UserId = userId };
            return View("PartOneIntro", viewModel);
        }

        [HttpGet]
        public IActionResult Question(string userId)
        {
            var nextQuesiton = QuestionService.GetNextQuestion(userId);
            var question = Mapper.Map<QuestionViewModelOut>(nextQuesiton);

            return View("Question", question);
        }

        [HttpPost]
        public IActionResult Submit(string userId, QuestionViewModelIn response)
        {
            QuestionService.AddAnswer(userId, response.Response);
            return RedirectToAction("Question", "Question", new { userId });
        }
    }
}