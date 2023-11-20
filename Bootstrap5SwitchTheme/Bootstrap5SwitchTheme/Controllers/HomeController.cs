using Bogus;
using Bootstrap5SwitchTheme.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Bootstrap5SwitchTheme.Controllers
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
            var faker = new Faker("en");
            var Ids = 0;
            var customModel = new Faker<CustomViewModel>()
              .RuleFor(p => p.pk, f => Ids++)
              .RuleFor(p => p.name, f => f.Company.CompanyName());

            var names = from e in customModel.Generate(5)
                             select new
                                {
                                    id = e.pk,
                                    name = e.name
                                };
            ViewData["names"] = new SelectList(names, "id", "name");
            return View();
        }
        
        [HttpGet]
        public JsonResult GetFakeGridData(string sortBy, string direction, int page, int limit, string searchString, int recid)
        {
            var faker = new Faker("en");
            var Ids = 1;
            var customModel = new Faker<CustomViewModel>()
              .RuleFor(p => p.pk, f => Ids++)
              .RuleFor(p => p.name, f => f.Company.CompanyName());

            var names = from e in customModel.Generate(5)
                        select new
                        {
                            recid = e.pk,
                            name = e.name
                        };

            return Json(new { status = "success", total = names.Count(), records = names.ToList() });
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
