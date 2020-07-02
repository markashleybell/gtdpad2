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
    [ApiController]
    [Route("api/[controller]")]
    public class ListsController : ControllerBase
    {
        private readonly ILogger<ListsController> _logger;
        private readonly IRepository _repository;

        public ListsController(ILogger<ListsController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateListViewModel model)
        {
            var section = new List(
                id: Guid.NewGuid(),
                owner: UserID,
                title: model.Title,
                order: model.Order
            );

            await _repository.PersistList(section, model.PageID);

            return Json(section);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(UpdateListViewModel model)
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

            return Json(updatedSection);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DeleteListViewModel model)
        {
            await _repository.DeleteList(model.ID);

            return Json(model);
        }
    }
}
