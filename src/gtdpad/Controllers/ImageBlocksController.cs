using System;
using System.Linq;
using System.Threading.Tasks;
using gtdpad.Domain;
using gtdpad.Dto;
using gtdpad.Services;
using gtdpad.Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public async Task<IActionResult> Get()
        {
            var imageBlocks = await Repository.GetImageBlocks(UserID);

            return Ok(imageBlocks.Select(ImageBlockDto.FromImageBlock));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var section = await Repository.GetImageBlock(id);

            return section is null
                ? (IActionResult)NotFound()
                : Ok(ImageBlockDto.FromImageBlock(section));
        }

        [HttpPost]
        public async Task<IActionResult> Post(ImageBlockDto dto)
        {
            Guard.AgainstNull(dto, nameof(dto));

            var section = new ImageBlock(
                id: Guid.NewGuid(),
                page: dto.Page,
                owner: UserID,
                title: dto.Title,
                order: dto.Order
            );

            await Repository.PersistImageBlock(section);

            return CreatedAtAction(nameof(Get), dto.WithID(section.ID));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ImageBlockDto dto)
        {
            Guard.AgainstNull(dto, nameof(dto));

            var section = await Repository.GetImageBlock(id);

            if (section is null)
            {
                return NotFound();
            }

            var updatedSection = section.With(
                title: dto.Title,
                order: dto.Order
            );

            await Repository.PersistImageBlock(updatedSection);

            return Ok(dto);
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