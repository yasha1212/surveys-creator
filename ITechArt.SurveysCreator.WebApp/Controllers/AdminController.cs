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
        private const int UsersPageSize = 10;

        private readonly ILogger<AdminController> _logger;
        private readonly IUserService _userService;

        public AdminController(ILogger<AdminController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            _logger.LogInformation("Opening Admin/Index page");

            var totalPagesCount = await _userService.GetUserPagesCountAsync(UsersPageSize);

            var pagesInfo = new PagesInfo
            {
                PageNumber = pageNumber <= (totalPagesCount) && (pageNumber >= 1) ? pageNumber : 1,
                PageSize = UsersPageSize,
                TotalPagesCount = totalPagesCount
            };

            var usersInfo = await _userService.GetUsersInfoAsync(pagesInfo);

            var model = new UsersPagesViewModel
            {
                UsersInfo = usersInfo,
                PagesInfo = pagesInfo
            };

            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            _logger.LogInformation("Opening Admin/Edit page");

            if (id == null)
            {
                return NotFound();
            }

            var allRoles = await _userService.GetRolesAsync();
            var userInfo = await _userService.GetUserInfoAsync(id);

            if (userInfo == null)
            {
                return NotFound();
            }

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

            if (! await _userService.ContainsByIdAsync(model.Id))
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

            await _userService.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            _logger.LogInformation("Opening Account/SignUp page");

            var allRoles = await _userService.GetRolesAsync();

            var model = new CreateUserViewModel
            {
                AllRoles = allRoles.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            model.AllRoles = await _userService.GetRolesAsync();

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
