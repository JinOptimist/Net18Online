using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models;

namespace WebPortalEverthing.Controllers
{
    public class AnimeCatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
