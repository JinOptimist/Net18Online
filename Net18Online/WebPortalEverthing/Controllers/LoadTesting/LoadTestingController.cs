using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.LoadTesting;

namespace WebPortalEverthing.Controllers.LoadTesting
{
    public class LoadTestingController : Controller
    {
        // Инициализация модели
        private static LoadTestingContentMetricsListViewModel model = new LoadTestingContentMetricsListViewModel(6);

        public IActionResult ContenMetricsListView()
        /*     decimal throughput,
             decimal average)
         /*  , int countMetrics)//входные параметры приходят снаружи от пользователя страницы */
        {


            /*       model.Metrics[0].Throughput = throughput;
                   model.Metrics[0].Average = average; */

            // Передаем модель в представление
            return View(model); //model выдаст данные наружу, на страницу
        }

        [HttpGet]
        public IActionResult CreateProfile()
        {
            Guid guid;
            string name;
            decimal throughput;
            decimal average;

            return View();
        }

        [HttpPost]
        public IActionResult CreateProfile
            (string name,
            decimal throughput,
            decimal average)
        {
            Guid guid;

            model.Metrics.Add(new Metric
            {
                Guid = Guid.NewGuid(),
                Name = name,
                Throughput = throughput * 1.0m,
                Average = average * 1.0m
            });
            return Redirect("/LoadTesting/ContenMetricsListView");
        }
    }
}
