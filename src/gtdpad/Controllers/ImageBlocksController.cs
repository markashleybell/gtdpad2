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
    public class ImageBlocksController : ControllerBase
    {
        private readonly ILogger<ImageBlocksController> _logger;
        private readonly IRepository _repository;

        public ImageBlocksController(ILogger<ImageBlocksController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> Create(CreateImageBlockViewModel model)
        {
            var section = new ImageBlock(
                id: Guid.NewGuid(),
                owner: UserID,
                title: model.Title,
                order: model.Order
            );

            await _repository.PersistImageBlock(section, model.PageID);

            return Content("OK: " + section.ID);
        }

        public async Task<IActionResult> Update(UpdateImageBlockViewModel model)
        {
            var section = await _repository.GetImageBlock(model.ID);

            if (section is null)
            {
                return NotFound();
            }

            var updatedSection = section.With(
                title: model.Title,
                order: model.Order
            );

            await _repository.PersistImageBlock(updatedSection, model.PageID);

            return Content("OK: " + updatedSection.ID);
        }

        public async Task<IActionResult> Delete(DeleteImageBlockViewModel model)
        {
            await _repository.DeleteImageBlock(model.ID);

            return Content("OK: " + model.ID);
        }
    }
}
