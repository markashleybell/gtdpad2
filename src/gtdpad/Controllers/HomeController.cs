using System.Linq;
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
            var pages = await _repository.GetPages(UserID);

            var index = pages.First();

            var model = new IndexViewModel {
                User = User.Identity.Name,
                PageID = index.ID,
                PageTitle = index.Title,
                Pages = pages
            };

            return View(model);
        }
    }
}
