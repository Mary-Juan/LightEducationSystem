using LightEducationSystem.Models;
using LightEducationSystem.Services.Interfaces;
using LightEducationSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LightEducationSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICurrentUserService _currentUserService;
        public HomeController(ILogger<HomeController> logger, IAuthenticationService authenticationService, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
            _currentUserService = currentUserService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
           var person = _authenticationService.Login(login);

            if (person == null)
            {
                ViewBag.NotRegistered = "User Not Found. please register first.";
                return RedirectToAction("Register");
            }

            _currentUserService.AddCurrentUser(person);

            if (person.role.Id == 2)
                return RedirectToAction("Index", "Student", person.Id);

            if (person.role.Id == 1)
                return RedirectToAction("Index", "Professor", person.Id);

            return BadRequest();
        }

        public IActionResult Register()
        {
            ViewBag.Roles = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value = "2",
                    Text = "Student"
                },
                new SelectListItem()
                {
                    Value = "1",
                    Text = "Professor"
                }
            };

            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
                return View(register);

            try
            {
                _authenticationService.Register(register);
                return RedirectToAction("Login");
            }
            catch
            {
                return BadRequest();
            }
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
