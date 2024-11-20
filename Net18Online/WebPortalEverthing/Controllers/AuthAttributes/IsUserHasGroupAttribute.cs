using Enums.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Controllers.AuthAttributes
{
    public class IsUserHasGroupAttribute : ActionFilterAttribute
    {
        private Role _role;
        public IsUserHasGroupAttribute(Role role)
        {
            _role = role;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var authService = context
                .HttpContext
                .RequestServices
                .GetRequiredService<AuthService>();

            if (!authService.IsUserHasGroup(_role))
            {
                context.Result = new ForbidResult();
                return;
            }

            base.OnActionExecuted(context);
        }
    }
}
