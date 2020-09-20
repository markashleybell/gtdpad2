using System;
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

        /*
        [AllowAnonymous]
        [Route("")]
        [Route("{id}")]
        [Route("users/login")]
        public IActionResult Index()
        {
            var model = new IndexViewModel();

            return View(model);
        }
        */

        [AllowAnonymous]
        [Route("routes")]
        public IActionResult Routes() => View();
    }
}
