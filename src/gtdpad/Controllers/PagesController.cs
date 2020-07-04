using System;
using System.Threading.Tasks;
using gtdpad.Domain;
using gtdpad.Models;
using gtdpad.Services;
using gtdpad.Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gtdpad.Controllers
{
    public class PagesController : ApiControllerBase<PagesController, Page>
    {
        public PagesController(
            ILogger<PagesController> logger,
            IRepository repository)
            : base(
                logger,
                repository)
        { }

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await Repository.GetPages(UserID));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var page = await Repository.GetPage(id);

            return page is null
                ? (IActionResult)NotFound()
                : Ok(page);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreatePageViewModel model)
        {
            Guard.AgainstNull(model, nameof(model));

            var page = new Page(
                id: Guid.NewGuid(),
                owner: UserID,
                title: model.Title,
                slug: model.Slug,
                order: model.Order
            );

            await Repository.PersistPage(page);

            return CreatedAtAction(nameof(Get), new { id = page.ID }, page);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdatePageViewModel model)
        {
            Guard.AgainstNull(model, nameof(model));

            var page = await Repository.GetPage(id);

            if (page is null)
            {
                return NotFound();
            }

            var updatedPage = page.With(
                title: model.Title,
                slug: model.Slug,
                order: model.Order
            );

            await Repository.PersistPage(updatedPage);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var page = await Repository.GetPage(id);

            if (page is null)
            {
                return NotFound();
            }

            await Repository.DeletePage(id);

            return NoContent();
        }
    }
}
