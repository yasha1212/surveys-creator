using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ITechArt.SurveysCreator.DAL.Models;
using ITechArt.SurveysCreator.Foundation.Services;
using ITechArt.SurveysCreator.WebApp.ViewModels;
using Microsoft.Extensions.Logging;

namespace ITechArt.SurveysCreator.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserService _userService;

        public AccountController(ILogger<AccountController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult SignUp()
        {
            _logger.LogInformation("Opening Account/SignUp page");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User()
            {
                Email = model.Email, 
                UserName = model.Email,
                FirstName = model.FirstName,
                SecondName = model.SecondName
            };

            var result = await _userService.SignUpAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }

            return RedirectToAction("Index", "Surveys");
        }

        public IActionResult Login()
        {
            _logger.LogInformation("Opening Account/Login page");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _userService.SignInAsync(model.Email, model.Password);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "The email or password you entered is invalid!");

                return View(model);
            }

            return RedirectToAction("MySurveys", "Surveys");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation("Opening Account/Logout page");

            await _userService.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public JsonResult IsUnique(string email)
        {
            if (_userService.ContainsByEmail(email))
            {
                return Json(false);
            }

            return Json(true);
        }
    }
}
