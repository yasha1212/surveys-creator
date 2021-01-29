﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ITechArt.SurveysCreator.DAL.Models;
using ITechArt.SurveysCreator.Foundation.Services;
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
        private readonly IUserService _userService;

        public AccountController(ILogger<AccountController> logger, IUserService userService,
            UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
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

            var user = new User() {Email = model.Email, UserName = model.Email};
            var result = await _userManager.CreateAsync(user, model.Password);

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

        [HttpPost]
        public JsonResult IsUnique(string email)
        {
            List<User> users = _userService.Get().ToList();

            string existingEmail = users.Where(u => u.NormalizedEmail == email.ToUpper())
                                   .Select(u => u.Email).FirstOrDefault();

            if (existingEmail != null)
            {
                return Json(false);
            }

            return Json(true);
        }
    }
}
