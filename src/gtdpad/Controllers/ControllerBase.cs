using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace gtdpad.Controllers
{
    public class ControllerBase : Controller
    {
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

        public override PartialViewResult PartialView(string name, object model) =>
            base.PartialView($"_{name}", model);
    }
}
