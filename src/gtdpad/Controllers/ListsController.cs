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
    public class ListsController : ControllerBase
    {
        private readonly ILogger<ListsController> _logger;
        private readonly IRepository _repository;

        public ListsController(ILogger<ListsController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> Create(CreateListViewModel model)
        {
            var section = new List(
                id: Guid.NewGuid(),
                owner: UserID,
                title: model.Title,
                order: model.Order
            );

            await _repository.PersistList(section, model.PageID);

            return Content("OK: " + section.ID);
        }

        public async Task<IActionResult> Update(UpdateListViewModel model)
        {
            var section = await _repository.GetList(model.ID);

            if (section is null)
            {
                return NotFound();
            }

            var updatedSection = section.With(
                title: model.Title,
                order: model.Order
            );

            await _repository.PersistList(updatedSection, model.PageID);

            return Content("OK: " + updatedSection.ID);
        }

        public async Task<IActionResult> Delete(DeleteListViewModel model)
        {
            await _repository.DeleteList(model.ID);

            return Content("OK: " + model.ID);
        }
    }
}
