using System;
using System.Diagnostics;
using AutoMapper;
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
            var user = Mapper.Map<RegistrationModel>(viewModel);
            user.Id = Guid.NewGuid();
            //RegistrationService.RegisterUser(user);

            return RedirectToAction("Part", "Question", new { part = 1, userId = user.Id });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
