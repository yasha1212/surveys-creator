using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ITechArt.SurveysCreator.DAL.Models;
using ITechArt.SurveysCreator.WebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ITechArt.SurveysCreator.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(ILogger<AccountController> logger,
            UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            _logger.LogInformation("Opening Account/Login page");

            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

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

            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }
    }
}
