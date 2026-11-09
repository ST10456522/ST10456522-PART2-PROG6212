using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Models;
using RaceDayMVC.Services;

namespace RaceDayMVC.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly RaceDayApiClient _api;

        public ParticipantController(RaceDayApiClient api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Participant")
                return RedirectToAction("Login", "Account");

            var allEvents = await _api.GetEventsAsync();
            var upcoming = allEvents.Where(e => e.EventDate >= DateTime.Today).OrderBy(e => e.EventDate).ToList();

            string enrolledStr = HttpContext.Session.GetString("EnrolledEvents") ?? "";
            ViewBag.EnrolledEvents = enrolledStr.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

            // Pass the categories to the view for the dropdown
            ViewBag.Categories = new List<CategoryModel>
            {
                new CategoryModel { CategoryId = 1, CategoryName = "Open / Senior" },
                new CategoryModel { CategoryId = 2, CategoryName = "Junior (Under 18)" },
                new CategoryModel { CategoryId = 3, CategoryName = "Veteran (40+)" }
            };

            return View(upcoming);
        }

        [HttpPost]
        public async Task<IActionResult> Enrol(int eventId, int categoryId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            string result = await _api.EnrolAsync(eventId, userId.Value, categoryId);

            if (result.ToLower().Contains("success") || result.ToLower().Contains("saved"))
            {
                string enrolledStr = HttpContext.Session.GetString("EnrolledEvents") ?? "";
                if (!enrolledStr.Contains(eventId.ToString()))
                {
                    enrolledStr += eventId.ToString() + ",";
                    HttpContext.Session.SetString("EnrolledEvents", enrolledStr);
                }
                TempData["Success"] = "You have successfully enrolled in the event!";
            }
            else
            {
                TempData["Success"] = "Enrolment result: " + result;
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Participant") return RedirectToAction("Login", "Account");
            var allEvents = await _api.GetEventsAsync();
            var ev = allEvents.FirstOrDefault(e => e.EventId == id);
            if (ev == null) return RedirectToAction("Index");

            ViewBag.Categories = new List<CategoryModel>
            {
                new CategoryModel { CategoryId = 1, CategoryName = "Open / Senior" },
                new CategoryModel { CategoryId = 2, CategoryName = "Junior (Under 18)" },
                new CategoryModel { CategoryId = 3, CategoryName = "Veteran (40+)" }
            };
            return View(ev);
        }
        [HttpGet]
        public async Task<IActionResult> MyEnrolments()
        {
            if (HttpContext.Session.GetString("UserRole") != "Participant")
                return RedirectToAction("Login", "Account");

            // 1. Get all events from API
            var allEvents = await _api.GetEventsAsync();

            // 2. Get the list of event IDs the user enrolled in from the Session
            string enrolledStr = HttpContext.Session.GetString("EnrolledEvents") ?? "";
            var enrolledIds = enrolledStr.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

            // 3. Filter the events to only show the ones they enrolled in
            var myEvents = allEvents.Where(e => enrolledIds.Contains(e.EventId.ToString())).ToList();

            return View(myEvents);
        }

        public IActionResult MyResults() => View();
        public IActionResult Profile() => View();
    }

}