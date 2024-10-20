using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.LoadTesting;

namespace WebPortalEverthing.Controllers.LoadTesting
{
    public class LoadTestingController : Controller
    {
        public IActionResult contentView1()
        {
            // Инициализация модели
            var model = new LoadTestingContentViewModel1();

            // Передаем модель в представление
            return View(model);
        }
    }
}
