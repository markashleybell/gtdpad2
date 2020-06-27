using System;
using System.Threading.Tasks;
using gtdpad.Models;
using gtdpad.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gtdpad.Controllers
{
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repository;

        public HomeController(ILogger<HomeController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var page = await _repository.GetPage(new Guid("796ac644-ff8e-47da-bbdc-ca8f822ce5f6"));

            var model = new IndexViewModel {
                User = User.Identity.Name,
                PageTitle = page.Title
            };

            return View(model);
        }
    }
}
