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
    public class DatabaseController : Controller
    {
        private readonly IEntityService _userService;
        private readonly ILogger<DatabaseController> _logger;

        public DatabaseController(IEntityService service, ILogger<DatabaseController> logger)
        {
            _userService = service;
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            _logger.LogInformation("Opening Database/Index page");
            
            return View(_userService.Get());
        }

        public IActionResult Create()
        {
            _logger.LogInformation("Opening Database/Create page");

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
            _logger.LogInformation("Opening Database/Edit page");

            if (id != null)
            {
                var user = _userService.Details((int)id);

                if (user != null)
                {
                    return View(user);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(int id, User user)
        {
            if (ModelState.IsValid)
            {
                _userService.Edit(id, user);

                return RedirectToAction("Index");
            }

            return View(user);
        }

        public IActionResult Delete(int? id)
        {
            _logger.LogInformation("Opening Database/Delete page");

            if (id != null)
            {
                var user = _userService.Details((int)id);

                if (user != null)
                {
                    return View(user);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);

            return RedirectToAction("Index");
        }

        public IActionResult Details(int? id)
        {
            _logger.LogInformation("Opening Database/Details page");

            if (id != null)
            {
                var user = _userService.Details((int)id);

                if (user != null)
                {
                    return View(user);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
