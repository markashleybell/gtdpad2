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
    public class ImageBlocksController : ApiControllerBase<ImageBlocksController, ImageBlock>
    {
        public ImageBlocksController(
            ILogger<ImageBlocksController> logger,
            IRepository repository)
            : base(
                logger,
                repository)
        { }

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await Repository.GetImageBlocks(UserID));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var section = await Repository.GetImageBlock(id);

            return section is null
                ? (IActionResult)NotFound()
                : Ok(section);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateImageBlockViewModel model)
        {
            Guard.AgainstNull(model, nameof(model));

            var section = new ImageBlock(
                id: Guid.NewGuid(),
                owner: UserID,
                title: model.Title,
                order: model.Order
            );

            await Repository.PersistImageBlock(section, model.PageID);

            return CreatedAtAction(nameof(Get), section);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateImageBlockViewModel model)
        {
            Guard.AgainstNull(model, nameof(model));

            var section = await Repository.GetImageBlock(id);

            if (section is null)
            {
                return NotFound();
            }

            var updatedSection = section.With(
                title: model.Title,
                order: model.Order
            );

            await Repository.PersistImageBlock(updatedSection, model.PageID);

            return Ok(updatedSection);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var list = await Repository.GetImageBlock(id);

            if (list is null)
            {
                return NotFound();
            }

            await Repository.DeleteImageBlock(id);

            return NoContent();
        }
    }
}
