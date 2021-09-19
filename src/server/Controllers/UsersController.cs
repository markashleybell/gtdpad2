using System;
using System.Threading.Tasks;
using gtdpad.Auth;
using gtdpad.Services;
using gtdpad.Support;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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

            var (valid, response) = await _userService.Authenticate(
                email: request.Email,
                password: request.Password,
                getExpires: () => _dateTimeService.Now.AddDays(1)
            );

            return !valid
                ? (IActionResult)Unauthorized()
                : Ok(response);
        }
    }
}
