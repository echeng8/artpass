using artpass.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using match_data_scraper;

namespace artpass.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            // test scraper 
           var matches = await TrackLockFetcher.FetchAndParseDataAsync("https://tracklock.gg/players/94516027");
            return View(matches);
        }

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
