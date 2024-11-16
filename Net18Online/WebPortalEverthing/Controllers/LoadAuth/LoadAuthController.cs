using Everything.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebPortalEverthing.Controllers.LoadAuth
{
    public class LoadAuthController : Controller
    {
        public ILoadUserRepositryReal _loadUserRepositryReal;

        public LoadAuthController(ILoadUserRepositryReal loadUserRepositryReal)
        {
            _loadUserRepositryReal = loadUserRepositryReal;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}