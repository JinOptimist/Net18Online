using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Services.LoadTesting;

namespace WebPortalEverthing.Controllers.LoadTesting.Attribute
{
    public class IsLoadAuthenticated : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var loadAuthService = context.HttpContext.RequestServices.GetRequiredService<LoadAuthService>();
            if (!loadAuthService.IsAuthenticated())
            {
                context.Result = new ForbidResult();
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
