using System;
using System.Diagnostics;
using Common;
using Microsoft.AspNetCore.Mvc;
using Interview.Models;
using Services;

namespace Interview.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(StringManipulationService stringManipulationService)
        {
            _stringManipulationService = stringManipulationService;
        }

        private readonly StringManipulationService _stringManipulationService;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "A .NET Core 2.0 Application.";

            return View();
        }       

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public JsonResult StringManipulation(string phrase, StringManipulationFunction task)
        {
            if (string.IsNullOrEmpty(phrase)) return Json(new {success = false, message = "Phrase is empty"});
            string resultString;
            switch (task)
            {
                case StringManipulationFunction.Uppercase:
                    resultString = _stringManipulationService.UppercaseAllLettersInString(phrase);
                    break;
                case StringManipulationFunction.Reverse:
                    resultString = _stringManipulationService.ReverseString(phrase);
                    break;
                default:
                    resultString = null;
                    break;
            }
            return Json(new {success = true, result = resultString});
        }                
    }
}
