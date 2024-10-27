using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.LoadTesting.GameLife;
using Everything.Data.Interface.Repositories;

namespace WebPortalEverthing.Controllers.GameLife
{
    public class GameLifeController : Controller
    {
        private IGameLifeRepository _gameLifeRepository;

        static int defWidth = 3;
        static int defHigth = 3;
        Field field = new Field(defWidth, defHigth);

        public GameLifeController(IGameLifeRepository gameLifeRepository)
        {
            _gameLifeRepository = gameLifeRepository;
        }

        public IActionResult GameLifeDefault()
        {

            field.Randomize();
            return View(field);
        }

        [HttpGet]
        public IActionResult GameLifeOwnSize()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GameLifeOwnSize(int width, int height)
        {
            if (width != defWidth || height != defHigth)
            {
                field = new Field(width, height);
                field.Randomize();
            }
            return View(field);
        }
    }
}