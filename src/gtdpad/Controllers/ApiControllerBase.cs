using System;
using gtdpad.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gtdpad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiControllerBase<TController, TEntity> : Controller
    {
        public ApiControllerBase(
            ILogger<TController> logger,
            IRepository repository)
        {
            Logger = logger;
            Repository = repository;
        }

        protected ILogger<TController> Logger { get; }

        protected IRepository Repository { get; }

        protected Guid UserID => new Guid("DF77778F-2EF3-49AF-A1A8-B1F064891EF5");
    }
}
