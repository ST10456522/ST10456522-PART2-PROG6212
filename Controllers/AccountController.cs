
using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Models;
using RaceDayMVC.Services;

namespace RaceDayMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly RaceDayApiClient api;

        public AccountController(RaceDayApiClient apiClient)
        {
            api = apiClient;
        }

        // ==========================================
        // LOGIN
        // ==========================================

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserRole") != null)
            {
                if (HttpContext.Session.GetString("UserRole") == "Organiser")
                    return RedirectToAction("Dashboard", "Organiser");

                else
                    return RedirectToAction("Index", "Participant");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Please fill in all fields correctly.";
                return View(model);
            }

            var result = await api.LoginAsync(
                model.Username,
                model.Password
            );

            if (result.Success)
            {
                HttpContext.Session.SetString("UserRole", result.Role);
                HttpContext.Session.SetInt32("UserId", result.UserId);
                HttpContext.Session.SetString("Username", model.Username);

                if (result.Role == "Organiser")
                    return RedirectToAction("Dashboard", "Organiser");

                else
                    return RedirectToAction("Index", "Participant");
            }

            ViewBag.Error = result.Message;
            return View(model);
        }


        // ==========================================
        // REGISTER
        // ==========================================

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            // Check participant details
            if (model.Role == "Participant")
            {
                if (string.IsNullOrWhiteSpace(model.FullName))
                {
                    ModelState.AddModelError(
                        "FullName",
                        "Full name is required."
                    );
                }

                if (string.IsNullOrWhiteSpace(model.Email))
                {
                    ModelState.AddModelError(
                        "Email",
                        "Email is required."
                    );
                }
            }

            // Check organiser details
            else if (model.Role == "Organiser")
            {
                if (model.OrganiserId <= 0)
                {
                    ModelState.AddModelError(
                        "OrganiserId",
                        "A valid organiser ID is required."
                    );
                }

                if (string.IsNullOrWhiteSpace(model.OrganiserName))
                {
                    ModelState.AddModelError(
                        "OrganiserName",
                        "Organiser name is required."
                    );
                }
            }
            else
            {
                ModelState.AddModelError(
                    "Role",
                    "Please select a valid account type."
                );
            }

            // Check all required fields
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Please check the highlighted fields.";
                return View(model);
            }

            // Send registration details to Part 2 API
            string resultMessage = await api.RegisterAsync(
                model.Username,
                model.Password,
                model.Role,
                model.FullName ?? "",
                model.Email ?? "",
                model.OrganiserId,
                model.OrganiserName ?? ""
            );

            // Check API response
            if (resultMessage.Contains(
                "success",
                StringComparison.OrdinalIgnoreCase))
            {
                TempData["SuccessMessage"] = resultMessage;

                return RedirectToAction("Login");
            }

            ViewBag.Error = resultMessage;

            return View(model);
        }


        // ==========================================
        // LOGOUT
        // ==========================================

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}
