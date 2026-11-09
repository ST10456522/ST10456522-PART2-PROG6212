using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Models;
using RaceDayMVC.Services;

namespace RaceDayMVC.Controllers
{
    public class OrganiserController : Controller
    {
        private readonly RaceDayApiClient _api;

        public OrganiserController(RaceDayApiClient api)
        {
            _api = api;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            string role = HttpContext.Session.GetString("UserRole");
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (role != "Organiser" || userId == null) return RedirectToAction("Login", "Account");

            var allEvents = await _api.GetEventsAsync();
            return View(allEvents); // Show all events for now
        }

        [HttpGet]
        public async Task<IActionResult> CreateEvent(int? id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser") return RedirectToAction("Login", "Account");

            if (id.HasValue)
            {
                var allEvents = await _api.GetEventsAsync();
                var eventToEdit = allEvents.FirstOrDefault(e => e.EventId == id.Value);
                if (eventToEdit != null)
                {
                    ViewBag.IsEdit = true;
                    return View(eventToEdit);
                }
            }

            ViewBag.IsEdit = false;
            return View(new EventModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventModel model, bool isEdit)
        {
            if (model.EventDate.Year < 1900) model.EventDate = DateTime.Today;

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            string result;

            if (isEdit)
            {
                result = await _api.UpdateEventAsync(model.EventId, model.EventType ?? "Run", model.EventName ?? "Event", model.Location ?? "TBD", model.EventDescription ?? "Desc", model.Weather ?? "Clear", model.EventDate, model.Distance ?? "0km", userId.Value, 1);
            }
            else
            {
                Random rnd = new Random();
                result = await _api.CreateEventAsync(rnd.Next(1000, 9999), model.EventType ?? "Run", model.EventName ?? "Event", model.Location ?? "TBD", model.EventDescription ?? "Desc", model.Weather ?? "Clear", model.EventDate, model.Distance ?? "0km", userId.Value, 1);
            }

            if (result.Contains("successfully") || result.Contains("saved")) return RedirectToAction("Dashboard");

            ViewBag.Error = "Failed to save event: " + result;
            ViewBag.IsEdit = isEdit;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (HttpContext.Session.GetString("UserRole") != "Organiser" || userId == null)
                return RedirectToAction("Login", "Account");

            // Capture the result from the API
            string result = await _api.DeleteEventAsync(eventId, userId.Value);

            // Show a message so we know what happened
            if (result.ToLower().Contains("success"))
            {
                TempData["Success"] = "Event deleted successfully!";
            }
            else
            {
                TempData["Error"] = "Delete failed: " + result;
            }

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public IActionResult Categories(int id)
        {
            ViewBag.EventId = id;
            return View(new List<CategoryModel>());
        }
        [HttpGet]
        public IActionResult Enrolments(int id)
        {
            ViewBag.EventId = id;
            return View();
        }
        [HttpGet]
        public IActionResult CaptureResults(int id)
        {
            ViewBag.EventId = id;
            return View();
        }
    }
        
    
}