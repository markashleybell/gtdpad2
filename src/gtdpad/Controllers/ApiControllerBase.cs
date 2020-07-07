using gtdpad.Services;
using gtdpad.Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace gtdpad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiControllerBase<TController> : ControllerBase<TController>
    {
        public ApiControllerBase(
            ILogger<TController> logger,
            IRepository repository,
            IOptionsMonitor<Settings> optionsMonitor)
            : base(
                logger,
                repository,
                optionsMonitor)
        { }
    }
}
