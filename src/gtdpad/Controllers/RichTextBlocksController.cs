using System;
using System.Linq;
using System.Threading.Tasks;
using gtdpad.Domain;
using gtdpad.Dto;
using gtdpad.Services;
using gtdpad.Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace gtdpad.Controllers
{
    public class RichTextBlocksController : ApiControllerBase<RichTextBlocksController>
    {
        public RichTextBlocksController(
            ILogger<RichTextBlocksController> logger,
            IRepository repository,
            IOptionsMonitor<Settings> optionsMonitor)
            : base(
                logger,
                repository,
                optionsMonitor)
        { }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var richTextBlocks = await Repository.GetRichTextBlocks(UserID);

            return Ok(richTextBlocks.Select(RichTextBlockDto.FromRichTextBlock));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var section = await Repository.GetRichTextBlock(id);

            return section is null
                ? (IActionResult)NotFound()
                : Ok(RichTextBlockDto.FromRichTextBlock(section));
        }

        [HttpPost]
        public async Task<IActionResult> Post(RichTextBlockDto dto)
        {
            Guard.AgainstNull(dto, nameof(dto));

            var section = new RichTextBlock(
                id: Guid.NewGuid(),
                page: dto.Page,
                owner: UserID,
                title: dto.Title,
                text: dto.Text,
                order: dto.Order
            );

            await Repository.PersistRichTextBlock(section);

            return CreatedAtAction(nameof(Get), dto.WithID(section.ID));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, RichTextBlockDto dto)
        {
            Guard.AgainstNull(dto, nameof(dto));

            if (dto.ID != id)
            {
                return BadRequest();
            }

            var section = await Repository.GetRichTextBlock(id);

            if (section is null)
            {
                return NotFound();
            }

            var updatedSection = section.With(
                title: dto.Title,
                text: dto.Text,
                order: dto.Order
            );

            await Repository.PersistRichTextBlock(updatedSection);

            return Ok(dto);
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
