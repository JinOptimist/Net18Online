using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.LoadTesting.GameLife;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Controllers.GameLife
{
    public class GameLifeController : Controller
    {
         int defWidth = 3;
         int defHigth = 3;


        public IActionResult GameLifeDefault()
        {

            Field field = new Field(defWidth, defHigth);
            field.Randomize();
            return View(field);
        }

        [HttpGet]
        public IActionResult GameLifeOwnSize()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GameLifeOwnSize(Field field)
        {
            //   if (width != defWidth || higth != defHigth) { field = new Field(width, higth); }
            return Redirect("GameLifeDefault");
        }
    }
}