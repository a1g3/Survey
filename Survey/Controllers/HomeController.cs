using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Survey.Domain.Interfaces.Services;
using Survey.Domain.Models;
using Survey.Models;

namespace Survey.Controllers
{
    public class HomeController : Controller
    {
        public IRegistrationService RegistrationService { get; set; }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(HomeViewModel viewModel)
        {
            var user = new UserModel(viewModel.FirstName.ToUpper(), viewModel.BirthDate);
            RegistrationService.RegisterUser(user);

            return RedirectToAction("Part", "Question", new { userId = user.UserId });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
