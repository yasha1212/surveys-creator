﻿using System.Linq;
using System.Threading.Tasks;
using ITechArt.SurveysCreator.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using ITechArt.SurveysCreator.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ITechArt.SurveysCreator.WebApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(ILogger<AdminController> logger, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Opening Admin/Index page");

            var users = _userManager.Users.ToList();

            var usersInfo = users
                .Select (u => new ShowUserInfoViewModel
                    {
                        Id = u.Id,
                        Email = u.Email,
                        FirstName = u.FirstName,
                        SecondName = u.SecondName
                    })
                .ToList();

            foreach (var user in users)
            {
                var currentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                var userInfo = usersInfo.FirstOrDefault(ui => ui.Id == user.Id);

                userInfo.Role = currentRole;
            }

            return View(usersInfo);
        }

        public async Task<IActionResult> Edit(string id)
        {
            _logger.LogInformation("Opening Admin/Edit page");

            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var allRoles = _roleManager.Roles
                .Select(r => r.Name).ToList();

            var model = new ChangeUserInfoViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                Role = userRoles.FirstOrDefault(),
                AllRoles = allRoles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ChangeUserInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.SecondName = model.SecondName;

            var userRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRoleAsync(user, model.Role);

            if (model.Password != null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            await _userManager.DeleteAsync(user);

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }

            await _userManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            _logger.LogInformation("Opening Account/SignUp page");

            var allRoles = _roleManager.Roles
                .Select(r => r.Name).ToList();

            var model = new CreateUserViewModel
            {
                AllRoles = allRoles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
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

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }

            await _userManager.AddToRoleAsync(user, model.Role);

            return RedirectToAction("Index");
        }
    }
}
