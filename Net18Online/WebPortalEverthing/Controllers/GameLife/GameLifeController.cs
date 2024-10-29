using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.LoadTesting.GameLife;
using Everything.Data.Interface.Repositories;

namespace WebPortalEverthing.Controllers.GameLife
{
    public class GameLifeController : Controller
    {
        private IGameLifeRepository _gameLifeRepository;

        static int defWidth = 6;
        static int defHigth = 7;
        FieldViewModel field = new FieldViewModel(defWidth, defHigth);

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
                field = new FieldViewModel(width, height);
                field.Randomize();
            }
            return View(field);
        }
    }
}