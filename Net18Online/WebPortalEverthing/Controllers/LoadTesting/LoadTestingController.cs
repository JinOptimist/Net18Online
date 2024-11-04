using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.LoadTesting;
using Everything.Data.Fake.Repositories;
using Everything.Data.Interface.Repositories;
using Everything.Data;
using Everything.Data.Repositories;

namespace WebPortalEverthing.Controllers.LoadTesting
{
    public class LoadTestingController : Controller
    {
        private ILoadTestingRepositoryReal _loadTestingRepository;
        private WebDbContext _webDbContext;
        protected const int DEFAULT_METRICS_COUNT = 6;

        public LoadTestingController(ILoadTestingRepositoryReal loadTestingRepository, WebDbContext webDbContext)
        {
            _loadTestingRepository = loadTestingRepository;
            _webDbContext = webDbContext;
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

            //     var metricsFromRealDB = _webDbContext.Metrics.Where(x => x.Guid != Guid.Empty).ToList();  //брали из БД, теперь из репозитория (пока просто для примера условия)
            var metricsFromRealDB = _loadTestingRepository.GetAll();
            //if (metricsFromRealDB.Count == 0)
            if (!_loadTestingRepository.Any())
            {
                for (int i = 1; i <= DEFAULT_METRICS_COUNT; i++)
                {
                    var metricViewModel = new MetricData
                    {
                        Name = $"Metric {i}",
                        Throughput = i * 10.5m,
                        Average = i * 5.0m
                    };

                    var metricFromRealDB = new Everything.Data.Models.MetricData
                    {
                        Guid = metricViewModel.Guid,
                        Name = metricViewModel.Name,
                        Throughput = metricViewModel.Throughput,
                        Average = metricViewModel.Average
                    };
                    //  _webDbContext.Metrics.Add(metricFromRealDB);
                    //  _webDbContext.SaveChanges();
                    _loadTestingRepository.Add(metricFromRealDB);
                }
            }

            //Из дата моделей делаем вьюмодели (список вью моделей)
            var metricsViewModel = metricsFromRealDB
                .Select(metricDB => new MetricViewModel
                {
                    Guid = metricDB.Guid,
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
        public IActionResult CreateProfileView(MetricViewModel metric)
        {

            var metricData = new Everything.Data.Models.MetricData
            {
                Name = metric.Name,
                Throughput = metric.Throughput * 1.0m,
                Average = metric.Average * 1.0m
            };
            // _webDbContext.Metrics.Add(metricData);
            //_webDbContext.SaveChanges();
            _loadTestingRepository.Add(metricData);

            return Redirect("/LoadTesting/ContenMetricsListView");
        }


        public IActionResult UpdateNameById(int id, string newName)
        {
            _loadTestingRepository.UpdateNameById(id, newName);
            return RedirectToAction("/LoadTesting/ContenMetricsListView");
        }


        public IActionResult UpdateNameByGuid(Guid Guid, string newName)
        {
            _loadTestingRepository.UpdateNameByGuid(Guid, newName);
            return RedirectToAction("/LoadTesting/ContenMetricsListView");
        }


        public IActionResult UpdateThroughputById(int Id, decimal Throughput)
        {
            _loadTestingRepository.UpdateThroughputById(Id, Throughput);
            return RedirectToAction("/LoadTesting/ContenMetricsListView");
        }

        public IActionResult UpdateThroughputByGuid(Guid Guid, decimal Throughput)
        {
            _loadTestingRepository.UpdateThroughputByGuid(Guid, Throughput);
            return RedirectToAction("/LoadTesting/ContenMetricsListView");
        }

        public IActionResult Remove(int id)
        {
            _loadTestingRepository.Delete(id);
            return RedirectToAction("/LoadTesting/ContenMetricsListView");
        }




    }
}
