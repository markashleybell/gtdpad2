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
        public async Task<IActionResult> Get()
        {
            var lists = await Repository.GetLists(UserID);

            return Ok(lists.Select(ListDto.FromList));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var section = await Repository.GetList(id);

            return section is null
                ? (IActionResult)NotFound()
                : Ok(ListDto.FromList(section));
        }

        [HttpPost]
        public async Task<IActionResult> Post(ListDto dto)
        {
            Guard.AgainstNull(dto, nameof(dto));

            var section = new List(
                id: Guid.NewGuid(),
                page: dto.Page,
                owner: UserID,
                title: dto.Title,
                order: dto.Order
            );

            await Repository.PersistList(section);

            return CreatedAtAction(nameof(Get), dto.WithID(section.ID));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ListDto dto)
        {
            Guard.AgainstNull(dto, nameof(dto));

            if (dto.ID != id)
            {
                return BadRequest();
            }

            var section = await Repository.GetList(id);

            if (section is null)
            {
                return NotFound();
            }

            var updatedSection = section.With(
                title: dto.Title,
                order: dto.Order
            );

            await Repository.PersistList(updatedSection);

            return Ok(dto);
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