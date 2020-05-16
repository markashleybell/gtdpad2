using gtdpad.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gtdpad.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) =>
            _logger = logger;

        public IActionResult Index()
        {
            var model = new IndexViewModel {
                User = User.Identity.Name
            };

            return View(model);
        }
    }
}
