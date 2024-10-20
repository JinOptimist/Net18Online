using Microsoft.AspNetCore.Mvc;

namespace WebPortalEverthing.Controllers.LoadTesting
{
    public class LoadTestingController : Controller
    {
        public IActionResult contentView1()
        {
            return View();
        }
    }
}
