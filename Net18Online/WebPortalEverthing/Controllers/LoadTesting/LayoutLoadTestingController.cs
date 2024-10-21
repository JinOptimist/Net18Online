using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.LoadTesting;

namespace WebPortalEverthing.Controllers.LoadTesting
{
    public class LayoutLoadTestingController : Controller
    {
        public IActionResult _LayoutLoadTesting()
        {
            // Инициализация модели
            var model = new _LayoutLoadTestingModel();

            // Передаем модель в представление
            return View(model); //model выдаст данные наружу, на страницу

        }
    }
}
