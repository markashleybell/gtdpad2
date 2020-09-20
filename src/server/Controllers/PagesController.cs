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
    [Route("api/pages")]
    public class PagesController : ApiControllerBase<PagesController>
    {
        public PagesController(
            ILogger<PagesController> logger,
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
            var pages = await Repository.GetPages(UserID);

            return Ok(pages.Select(PageDto.FromPage));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var page = await Repository.GetPage(id);

            return page is null
                ? (IActionResult)NotFound()
                : Ok(PageDto.FromPage(page));
        }

        [HttpPost]
        public async Task<IActionResult> Post(PageDto dto)
        {
            Guard.AgainstNull(dto, nameof(dto));

            var page = new Page(
                id: Guid.NewGuid(),
                owner: UserID,
                title: dto.Title,
                slug: dto.Slug,
                order: dto.Order
            );

            await Repository.PersistPage(page);

            return CreatedAtAction(nameof(Get), new { id = page.ID }, dto.WithID(page.ID));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, PageDto dto)
        {
            Guard.AgainstNull(dto, nameof(dto));

            var page = await Repository.GetPage(id);

            if (page is null)
            {
                return NotFound();
            }

            var updatedPage = page.With(
                title: dto.Title,
                slug: dto.Slug,
                order: dto.Order
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
