using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ITechArt.SurveysCreator.WebApp.Controllers
{
    public class SurveysController : Controller
    {
        private readonly ILogger<SurveysController> _logger;

        public SurveysController(ILogger<SurveysController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Opening Surveys/Index page");

            return View();
        }
    }
}
