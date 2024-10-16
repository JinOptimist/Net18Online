using Microsoft.AspNetCore.Mvc;

namespace WebPortalEverthing.Controllers
{
    public class AnimeGirlController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllGirls()
        {
            return View();
        }
    }
}
