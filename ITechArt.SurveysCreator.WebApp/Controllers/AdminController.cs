using System.Linq;
using System.Threading.Tasks;
using ITechArt.SurveysCreator.DAL.Models;
using ITechArt.SurveysCreator.Foundation.Models;
using ITechArt.SurveysCreator.Foundation.Services;
using Microsoft.AspNetCore.Mvc;
using ITechArt.SurveysCreator.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace ITechArt.SurveysCreator.WebApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IUserService _userService;

        public AdminController(ILogger<AdminController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Opening Admin/Index page");

            var usersInfo = _userService.GetUsersInfo();

            return View(usersInfo);
        }

        public IActionResult Edit(string id)
        {
            _logger.LogInformation("Opening Admin/Edit page");

            if (id == null)
            {
                return NotFound();
            }

            if (!_userService.ContainsById(id))
            {
                return NotFound();
            }

            var allRoles = _userService.GetRoles();
            var userInfo = _userService.GetUserInfo(id);

            var model = new ChangeUserInfoViewModel
            {
                Email = userInfo.Email,
                FirstName = userInfo.FirstName,
                SecondName = userInfo.SecondName,
                Role = userInfo.Role,
                AllRoles = allRoles.ToList()
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

            if (!_userService.ContainsById(model.Id))
            {
                return NotFound();
            }

            var result = await _userService.EditAsync(new UserInfo
            {
                Id = model.Id,
                Email = model.Email,
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                Role = model.Role
            }, model.Password);

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
                return NotFound();
            }

            if (!_userService.ContainsById(id))
            {
                return NotFound();
            }

            await _userService.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            _logger.LogInformation("Opening Account/SignUp page");

            var allRoles = _userService.GetRoles();

            var model = new CreateUserViewModel
            {
                AllRoles = allRoles.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            model.AllRoles = _userService.GetRoles().ToList();

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

            var result = await _userService.CreateAsync(user, model.Password, model.Role);

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
    }
}
