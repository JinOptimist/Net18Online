using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.LoadTesting;

namespace WebPortalEverthing.Controllers.LoadTesting
{
    public class LoadTestingController : Controller
    {
        public IActionResult contentView1(
            decimal Throughput,
            decimal Average)//входные параметры приходят снаружи от пользователя страницы
        {
            // Инициализация модели
            var model = new LoadTestingContentViewModel1();
            model.Metrics[0].Throughput = Throughput;
            model.Metrics[0].Average = Average;

            // Передаем модель в представление
            return View(model); //model выдаст данные наружу, на страницу
        }
    }
}
