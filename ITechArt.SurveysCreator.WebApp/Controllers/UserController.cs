using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArt.SurveysCreator.DAL.Models;
using ITechArt.SurveysCreator.Foundation.Services;
using Microsoft.Extensions.Logging;

namespace ITechArt.SurveysCreator.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService service, ILogger<UserController> logger)
        {
            _userService = service;
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            _logger.LogInformation("Opening User/Index page");
            
            return View(_userService.Get());
        }

        public IActionResult Create()
        {
            _logger.LogInformation("Opening User/Create page");

            var user = new User()
            {
                Age = 18,
                Email = "doenglish1@mail.ru",
                FirstName = "Lesha",
                SecondName = "Shukan",
                Login = "yasha1212",
            };

            return View(user);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            _userService.Add(user);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            _logger.LogInformation("Opening User/Edit page");

            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var user = _userService.Details((int)id);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            _userService.Edit(id, user);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            _logger.LogInformation("Opening User/Delete page");

            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var user = _userService.Details((int)id);

            if (user == null)
            {
                return RedirectToAction("Index");
            }
            
            return View(user);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);

            return RedirectToAction("Index");
        }

        public IActionResult Details(int? id)
        {
            _logger.LogInformation("Opening User/Details page");

            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var user = _userService.Details((int)id);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            return View(user);
        }
    }
}
