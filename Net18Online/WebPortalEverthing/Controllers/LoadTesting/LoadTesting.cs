using Microsoft.AspNetCore.Mvc;

namespace WebPortalEverthing.Controllers.LoadTesting
{
    public class LoadTesting : Controller
    {
        public IActionResult contentView1()
        {
            return View();
        }
    }
}
