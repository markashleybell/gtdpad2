using System;
using System.Threading.Tasks;
using gtdpad.Domain;
using gtdpad.Models;
using gtdpad.Services;
using gtdpad.Support;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static gtdpad.Constants;

namespace gtdpad.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly Settings _cfg;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDateTimeService _dateTimeService;
        private readonly IUserService _userService;

        public UsersController(
            IOptionsMonitor<Settings> optionsMonitor,
            IHttpContextAccessor httpContextAccessor,
            IDateTimeService dateTimeService,
            IUserService userService)
        {
            Guard.AgainstNull(optionsMonitor, nameof(optionsMonitor));

            _cfg = optionsMonitor.CurrentValue;
            _httpContextAccessor = httpContextAccessor;
            _dateTimeService = dateTimeService;
            _userService = userService;
        }

        [AllowAnonymous]
        public IActionResult Login(Uri returnUrl)
        {
            var model = new LoginViewModel {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Guard.AgainstNull(model, nameof(model));

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var (valid, id) = await _userService.ValidateLogin(model.Email, model.Password);

            if (!valid)
            {
                return View(model);
            }

            var principal = _userService.GetClaimsPrincipal(id.Value, model.Email);

            var authenticationProperties = new AuthenticationProperties {
                IsPersistent = true,
                ExpiresUtc = _dateTimeService.Now.AddDays(_cfg.PersistentSessionLengthInDays)
            };

            await _httpContextAccessor.HttpContext
                .SignInAsync(principal, authenticationProperties)
                .ConfigureAwait(false);

            var returnUrl = Url.IsNonEmptyAndLocal(model.ReturnUrl)
                ? model.ReturnUrl
                : SiteRootUri;

            return Redirect(returnUrl.ToString());
        }

        public async Task<IActionResult> Logout()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync();

            return Redirect(SiteRootUri.ToString());
        }
    }
}
