using System;
using System.Threading.Tasks;
using gtdpad.Auth;
using gtdpad.Domain;
using gtdpad.Models;
using gtdpad.Services;
using gtdpad.Support;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static gtdpad.Constants;

namespace gtdpad.Controllers
{
    [Route("api/users")]
    public class UsersController : ApiControllerBase<UsersController>
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IUserService _userService;

        public UsersController(
            ILogger<UsersController> logger,
            IRepository repository,
            IOptionsMonitor<Settings> optionsMonitor,
            IDateTimeService dateTimeService,
            IUserService userService)
            : base(
                logger,
                repository,
                optionsMonitor)
        {
            _dateTimeService = dateTimeService;
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(AuthenticateRequest request)
        {
            Guard.AgainstNull(request, nameof(request));

            var (valid, response) = await _userService.Authenticate(request.Email, request.Password);

            return !valid
                ? (IActionResult)BadRequest()
                : Ok(response);
        }
    }
}
