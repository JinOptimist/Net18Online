using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models;

namespace WebPortalEverthing.Controllers
{
    public class AnimeCatalogueController : Controller
    {
        public IActionResult Index(string name, int age)
        {
            return View();
        }
    }
}
