using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult MySurveys()
        {
            _logger.LogInformation("Opening Surveys/MySurveys page");

            return View();
        }
    }
}
