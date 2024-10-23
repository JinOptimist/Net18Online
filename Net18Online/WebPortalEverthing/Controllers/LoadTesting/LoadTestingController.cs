using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.LoadTesting;

namespace WebPortalEverthing.Controllers.LoadTesting
{
    public class LoadTestingController : Controller
    {
        // Инициализация модели
        private static LoadTestingContentMetricsListViewModel model = new LoadTestingContentMetricsListViewModel();

        public IActionResult ContenMetricsListView()
        /*     decimal throughput,
             decimal average)
         /*  , int countMetrics)//входные параметры приходят снаружи от пользователя страницы */
        {
            /*       model.Metrics[0].Throughput = throughput;
                    model.Metrics[0].Average = average; */
            if (model.Metrics.Count == 0)
            {
                for (int i = 1; i <= 6; i++)
                {
                    model.Metrics.Add(new Metric
                    {
                        Guid = Guid.NewGuid(),
                        Name = $"Metric {i}",
                        Throughput = i * 10.5m,
                        Average = i * 5.0m
                    });
                }
            }

            // Передаем модель в представление
            return View(model); //model выдаст данные наружу, на страницу
        }

        [HttpGet]
        public IActionResult CreateProfileView()
        {
            Guid guid;
            string name;
            decimal throughput;
            decimal average;

            return View();
        }

        [HttpPost]
        public IActionResult CreateProfileView
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
