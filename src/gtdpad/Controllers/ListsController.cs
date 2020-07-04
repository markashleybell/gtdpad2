using System;
using System.Threading.Tasks;
using gtdpad.Domain;
using gtdpad.Models;
using gtdpad.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using gtdpad.Support;

namespace gtdpad.Controllers
{
    public class ListsController : ApiControllerBase<ListsController, List>
    {
        public ListsController(
            ILogger<ListsController> logger,
            IRepository repository)
            : base(
                logger,
                repository)
        { }

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await Repository.GetLists(UserID));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var section = await Repository.GetList(id);

            return section is null
                ? (IActionResult)NotFound()
                : Ok(section);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateListViewModel model)
        {
            Guard.AgainstNull(model, nameof(model));

            var section = new List(
                id: Guid.NewGuid(),
                owner: UserID,
                title: model.Title,
                order: model.Order
            );

            await Repository.PersistList(section, model.PageID);

            return CreatedAtAction(nameof(Get), section);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateListViewModel model)
        {
            Guard.AgainstNull(model, nameof(model));

            var section = await Repository.GetList(id);

            if (section is null)
            {
                return NotFound();
            }

            var updatedSection = section.With(
                title: model.Title,
                order: model.Order
            );

            await Repository.PersistList(updatedSection, model.PageID);

            return Ok(updatedSection);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var list = await Repository.GetList(id);

            if (list is null)
            {
                return NotFound();
            }

            await Repository.DeleteList(id);

            return NoContent();
        }
    }
}
