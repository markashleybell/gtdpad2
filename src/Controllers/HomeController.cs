using gtdpad.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gtdpad.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) =>
            _logger = logger;

        public IActionResult Index() =>
            View(new IndexViewModel());
    }
}
