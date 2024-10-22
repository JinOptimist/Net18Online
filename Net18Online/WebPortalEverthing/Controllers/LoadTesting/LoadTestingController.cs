using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.LoadTesting;

namespace WebPortalEverthing.Controllers.LoadTesting
{
    public class LoadTestingController : Controller
    {
        public IActionResult ContenMetricsListView(
            decimal throughput,
            decimal average,
            int CountMetrics)//входные параметры приходят снаружи от пользователя страницы
        {
            // Инициализация модели
            var model = new LoadTestingContentMetricsListViewModel(CountMetrics);
            model.Metrics[0].Throughput = throughput;
            model.Metrics[0].Average = average;

            // Передаем модель в представление
            return View(model); //model выдаст данные наружу, на страницу
        }
    }
}
