using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Services;
using WebPortalEverthing.Services.LoadTesting;

namespace WebPortalEverthing.Controllers.LoadAdmin.LoadAdminAttrobute
{
    public class IsLoadAdminAttribute : ActionFilterAttribute
    {
        // проверка перед экшеном в контроллере
        public override void OnActionExecuted(ActionExecutedContext context) 
        {
            var loadAuthService = context
                .HttpContext
                .RequestServices
                .GetRequiredService<LoadAuthService>();
            if (!loadAuthService.IsAdmin())
            {
                context.Result = new ForbidResult();
                return;
            }

            base.OnActionExecuted(context);
        }
    }
}
