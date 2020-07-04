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
    public class RichTextBlocksController : ApiControllerBase<RichTextBlocksController, RichTextBlock>
    {
        public RichTextBlocksController(
            ILogger<RichTextBlocksController> logger,
            IRepository repository)
            : base(
                logger,
                repository)
        { }

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await Repository.GetRichTextBlocks(UserID));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var section = await Repository.GetRichTextBlock(id);

            return section is null
                ? (IActionResult)NotFound()
                : Ok(section);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateRichTextBlockViewModel model)
        {
            Guard.AgainstNull(model, nameof(model));

            var section = new RichTextBlock(
                id: Guid.NewGuid(),
                owner: UserID,
                title: model.Title,
                text: model.Text,
                order: model.Order
            );

            await Repository.PersistRichTextBlock(section, model.PageID);

            return CreatedAtAction(nameof(Get), section);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateRichTextBlockViewModel model)
        {
            Guard.AgainstNull(model, nameof(model));

            var section = await Repository.GetRichTextBlock(id);

            if (section is null)
            {
                return NotFound();
            }

            var updatedSection = section.With(
                title: model.Title,
                text: model.Text,
                order: model.Order
            );

            await Repository.PersistRichTextBlock(updatedSection, model.PageID);

            return Ok(updatedSection);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var list = await Repository.GetRichTextBlock(id);

            if (list is null)
            {
                return NotFound();
            }

            await Repository.DeleteRichTextBlock(id);

            return NoContent();
        }
    }
}
