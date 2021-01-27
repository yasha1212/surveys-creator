using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ITechArt.SurveysCreator.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            _logger.LogInformation("Opening Home/Index page");

            return View();
        }

        public IActionResult UserSurveys()
        {
            _logger.LogInformation("Opening Home/UserSurveys page");

            return View();
        }
    }
}
