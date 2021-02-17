using System.Linq;
using System.Threading.Tasks;
using ITechArt.SurveysCreator.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using ITechArt.SurveysCreator.Foundation.Services;
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

        public IActionResult Index()
        {
            _logger.LogInformation("Opening Admin/Index page");

            var users = _userManager.Users.ToList();

            return View(users);
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
            }

            return RedirectToAction("Index");
        }
    }
}
