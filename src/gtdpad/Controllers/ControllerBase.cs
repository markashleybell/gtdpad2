using System;
using System.Security.Claims;
using gtdpad.Services;
using gtdpad.Support;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace gtdpad.Controllers
{
    [Authorize]
    public class ControllerBase<TController> : Controller
    {
        public ControllerBase(
            ILogger<TController> logger,
            IRepository repository,
            IOptionsMonitor<Settings> optionsMonitor)
        {
            Guard.AgainstNull(optionsMonitor, nameof(optionsMonitor));

            Logger = logger;
            Repository = repository;
            Settings = optionsMonitor.CurrentValue;
        }

        protected ILogger<TController> Logger { get; }

        protected IRepository Repository { get; }

        protected Settings Settings { get; }

        protected Guid UserID
        {
            get
            {
                if (User?.Identity is null)
                {
                    return Guid.Empty;
                }

                var identity = User.Identity as ClaimsIdentity;

                var value = identity?.FindFirst(ClaimTypes.Sid)?.Value;

                return value is object ? new Guid(value) : Guid.Empty;
            }
        }
    }
}
