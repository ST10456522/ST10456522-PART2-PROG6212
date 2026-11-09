using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Models;
using System.Diagnostics;

namespace RaceDayMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
