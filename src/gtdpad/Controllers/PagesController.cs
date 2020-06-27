using System;
using System.Threading.Tasks;
using gtdpad.Domain;
using gtdpad.Models;
using gtdpad.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace gtdpad.Controllers
{
    [Authorize]
    public class PagesController : ControllerBase
    {
        private readonly ILogger<PagesController> _logger;
        private readonly IRepository _repository;

        public PagesController(ILogger<PagesController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> Create(CreatePageViewModel model)
        {
            var page = new Page(
                id: Guid.NewGuid(),
                owner: UserID,
                title: model.Title,
                slug: model.Slug,
                order: model.Order
            );

            await _repository.PersistPage(page);

            return Content("OK: " + page.ID);
        }

        public async Task<IActionResult> Update(UpdatePageViewModel model)
        {
            var page = await _repository.GetPage(model.ID);

            if (page is null)
            {
                return NotFound();
            }

            var updatedPage = page.With(
                title: model.Title,
                slug: model.Slug,
                order: model.Order
            );

            await _repository.PersistPage(updatedPage);

            return Content("OK: " + updatedPage.ID);
        }

        public async Task<IActionResult> Delete(DeletePageViewModel model)
        {
            await _repository.DeletePage(model.ID);

            return Content("OK: " + model.ID);
        }
    }
}
