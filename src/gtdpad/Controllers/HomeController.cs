using System.Linq;
using System.Threading.Tasks;
using gtdpad.Models;
using gtdpad.Services;
using gtdpad.Support;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace gtdpad.Controllers
{
    public class HomeController : ControllerBase<HomeController>
    {
        public HomeController(
            ILogger<HomeController> logger,
            IRepository repository,
            IOptionsMonitor<Settings> optionsMonitor)
            : base(
                logger,
                repository,
                optionsMonitor)
        { }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = new IndexViewModel();

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            var model = new LoginViewModel();

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Routes() => View();
    }
}
