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
    public class RichTextBlocksController : ControllerBase
    {
        private readonly ILogger<RichTextBlocksController> _logger;
        private readonly IRepository _repository;

        public RichTextBlocksController(ILogger<RichTextBlocksController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> Create(CreateRichTextBlockViewModel model)
        {
            var section = new RichTextBlock(
                id: Guid.NewGuid(),
                owner: UserID,
                title: model.Title,
                text: model.Text,
                order: model.Order
            );

            await _repository.PersistRichTextBlock(section, model.PageID);

            return Content("OK: " + section.ID);
        }

        public async Task<IActionResult> Update(UpdateRichTextBlockViewModel model)
        {
            var section = await _repository.GetRichTextBlock(model.ID);

            if (section is null)
            {
                return NotFound();
            }

            var updatedSection = section.With(
                title: model.Title,
                text: model.Text,
                order: model.Order
            );

            await _repository.PersistRichTextBlock(updatedSection, model.PageID);

            return Content("OK: " + updatedSection.ID);
        }

        public async Task<IActionResult> Delete(DeleteRichTextBlockViewModel model)
        {
            await _repository.DeleteRichTextBlock(model.ID);

            return Content("OK: " + model.ID);
        }
    }
}
