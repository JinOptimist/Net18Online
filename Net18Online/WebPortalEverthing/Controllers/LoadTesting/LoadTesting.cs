using Microsoft.AspNetCore.Mvc;

namespace WebPortalEverthing.Controllers.LoadTesting
{
    public class LoadTesting : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
