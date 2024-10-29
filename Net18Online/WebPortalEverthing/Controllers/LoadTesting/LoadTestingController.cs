using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.LoadTesting;
using Everything.Data.Fake.Repositories;
using Everything.Data.Interface.Repositories;

namespace WebPortalEverthing.Controllers.LoadTesting
{
    public class LoadTestingController : Controller
    {
        private ILoadTestingRepository _loadTestingRepository;
        protected const int DEFAULT_METRICS_COUNT = 6;

        public LoadTestingController(ILoadTestingRepository loadTestingRepository)
        {
            _loadTestingRepository = loadTestingRepository;
        }

        public IActionResult ContenMetricsListView()
        /*     decimal throughput,
             decimal average)
         /*  , int countMetrics)//входные параметры приходят снаружи от пользователя страницы */
        {
            /*       model.Metrics[0].Throughput = throughput;
                    model.Metrics[0].Average = average; */

            /* Передаем модель в представление
             Это datamodel(модель БД), На View можно отдавать только viewmodel(данные для пользователя не все или из др. датамоделей), нельзя datamodel */
            var metricsFromDB = _loadTestingRepository.GetAll();

            if (metricsFromDB.Count == 0)
            {
                for (int i = 1; i <= DEFAULT_METRICS_COUNT; i++)
                {
                    var metricViewModel = new MetricData
                    {
                        Name = $"Metric {i}",
                        Throughput = i * 10.5m,
                        Average = i * 5.0m
                    };
                    _loadTestingRepository.Add(metricViewModel);
                }
            }

            //Из дата моделей делаем вьюмодели (список вью моделей)
            var metricsViewModel = metricsFromDB
                .Select(metricDB => new Metric
                {
                    Average = metricDB.Average,
                    Throughput = metricDB.Throughput,
                    Name = metricDB.Name
                })
                .ToList();

            // Передаем модель в представление
            // На View можно отдавать только viewmodel, нельзя datamodel
            return View(metricsViewModel); //model выдаст данные наружу, на страницу
        }

        /*   [HttpGet]  нужен, чтобы отобразить страницу создания профайла перед заполнением полей */
        [HttpGet]
        public IActionResult CreateProfileView()
        {
            return View();
        }

        /* HttpPost  нужен, чтобы послать данные заполненные пользователем в экшен т.е. метрику (metric)  */
        [HttpPost]
        public IActionResult CreateProfileView(Metric metric)
        {
            var metricData = new MetricData
            {
                Name = metric.Name,
                Throughput = metric.Throughput * 1.0m,
                Average = metric.Average * 1.0m
            };
            _loadTestingRepository.Add(metricData);

            return Redirect("/LoadTesting/ContenMetricsListView");
        }

        /*   [HttpPost]
           public IActionResult _LayoutLoadTesting
               (string name,
               decimal throughput,
               decimal average)
           {
               var metricData = new MetricData
               {
                   Name = name,
                   Throughput = throughput * 1.0m,
                   Average = average * 1.0m
               };
               _loadTestingRepository.Add(metricData);

               return Redirect("/LoadTesting/ContenMetricsListView");
           }*/


    }
}
