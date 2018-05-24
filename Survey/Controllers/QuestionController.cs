using System;
using Microsoft.AspNetCore.Mvc;
using Survey.Domain.Interfaces.Services;
using Survey.Models;

namespace Survey.Controllers
{
    public class QuestionController : Controller
    {
        public IQuestionService QuestionService { get; set; }

        [HttpGet]
        public IActionResult Part(Guid userId)
        {
            var viewModel = new UserViewModel() { UserId = userId };
            return View("PartOneIntro", viewModel);
        }

        [HttpGet]
        public IActionResult Question(Guid userId)
        {
            //Add the question to the db
            //Get question count
            //If it is 15 then part 2 intro
            //If it is question #2,5,13,12,9,15 or 7 give them a question with random options
            //Generate a question and return it with the options
            return View("Question", new QuestionViewModelOut() { Question = "Choose a letter. There is no right answer." });
        }

        [HttpPost]
        public IActionResult Submit(Guid userId, QuestionViewModelIn reponse)
        {
            QuestionService.AddAnswer(new Guid(), "");
            return RedirectToAction("Question", "Question", new { userId });
        }
    }
}