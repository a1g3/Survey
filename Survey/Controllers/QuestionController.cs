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
        public IActionResult Part(int part, Guid userId)
        {
            if(part == 1)
            {
                return View("PartOneIntro");
            }
            return null;
        }

        [HttpGet]
        public IActionResult Question(int part, Guid userId)
        {
            //Add the question to the db
            //Get question count
            //If it is 15 then part 2 intro
            //If it is question #2,5,13,12,9,15 or 7 give them a question with random options
            //Generate a question and return it with the options
            return View("Question", new QuestionViewModelOut() { Question = "Hi" });
        }

        [HttpPost]
        public IActionResult Submit(int part, Guid questionId, QuestionViewModelIn reponse)
        {
            QuestionService.AddAnswer(new Guid(), "");
            return RedirectToAction("Question", "Question", new { part, questionId });
        }
    }
}