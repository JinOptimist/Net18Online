using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.LoadTesting;
using Everything.Data.Fake.Repositories;
using Everything.Data.Interface.Repositories;
using Everything.Data;
using Everything.Data.Repositories;
using System.Globalization;
using WebPortalEverthing.Services.LoadTesting;
using WebPortalEverthing.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortalEverthing.Controllers.AuthAttributes;
using WebPortalEverthing.Models.LoadTesting.Profile;
using WebPortalEverthing.Models.AnimeGirl.Profile;


namespace WebPortalEverthing.Controllers.LoadTesting
{
    public class LoadTestingController : Controller
    {
        private ILoadTestingRepositoryReal _loadTestingRepository;
        private ILoadVolumeTestingRepositoryReal _loadVolumeTestingRepositoryReal;
        private WebDbContext _webDbContext;
        protected const int DEFAULT_METRICS_COUNT = 6;

        private ILoadUserRepositryReal _loadUserRepositryReal;
        private LoadAuthService _loadAuthService;

        public LoadTestingController(ILoadTestingRepositoryReal loadTestingRepository, WebDbContext webDbContext, ILoadUserRepositryReal loadUserRepositryReal, LoadAuthService loadAuthService, ILoadVolumeTestingRepositoryReal loadVolumeTestingRepositoryReal)
        {
            _loadTestingRepository = loadTestingRepository;
            _webDbContext = webDbContext;
            _loadUserRepositryReal = loadUserRepositryReal;
            _loadAuthService = loadAuthService;
            _loadVolumeTestingRepositoryReal = loadVolumeTestingRepositoryReal;
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
            //if (metricsFromRealDB.Count == 0)
            if (!_loadTestingRepository.Any())
            {
                for (int i = 1; i <= DEFAULT_METRICS_COUNT; i++)
                {
                    var metricViewModel = new MetricData
                    {
                        Name = $"Metric {i}",
                        Throughput = i * 10.5m,
                        Average = i * 5.0m,

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

            var currentUserId = _loadAuthService.GetUserId();
            if (currentUserId is null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = _loadUserRepositryReal.Get(currentUserId.Value);

            var metricsFromRealDB = _loadTestingRepository.GetAllWithCreatorsAndLoadVolume();

            //Из дата моделей делаем вьюмодели (список вью моделей)
            var metricsViewModel = metricsFromRealDB
                .Select(metricDB => new MetricViewModel
                {
                    Id = metricDB.Id,
                    Guid = metricDB.Guid,
                    Average = metricDB.Average,
                    Throughput = metricDB.Throughput,
                    Name = metricDB.Name,
                    CreatorName = metricDB.LoadUserDataCreator?.Login ?? "UnknownCreator",
                    LoadVolumeName = metricDB.LoadVolumeTesting?.Title ?? "UnknownLoadVolume",
                    CanDelete = metricDB.LoadUserDataCreator is null
                    || metricDB.LoadUserDataCreator?.Id == currentUserId
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
            var viewModel = new MetricCreationViewModel();
            viewModel.LoadVolumes = _loadVolumeTestingRepositoryReal.GetAll()
                .Select(loadVolume =>
                new SelectListItem(loadVolume.Title, loadVolume.Id.ToString()))
                .ToList();

            return View(viewModel);
        }

        /* HttpPost  нужен, чтобы послать данные заполненные пользователем в экшен т.е. метрику (metric)  */
        [HttpPost]
        public IActionResult CreateProfileView(MetricCreationViewModel metric)
        {

            // Проверка модели
            if (!ModelState.IsValid)
            {
                return View(metric);
            }

            var currentUserId = _loadAuthService.GetUserId();

            // Создание объекта данных
            var metricData = new Everything.Data.Models.MetricData
            {
                Name = metric.Name,
                Throughput = (decimal)metric.Throughput,
                Average = (decimal)metric.Average
            };

            _loadTestingRepository.Create(metricData, currentUserId!.Value, metric.LoadVolumeId);

            return Redirect("/LoadTesting/ContenMetricsListView");
        }

        public IActionResult LoadUserProfile()
        {
            var viewModel = new LoadUserProfileViewModel();
            viewModel.UserName = _loadAuthService.GetName()!;

            var userId = _loadAuthService.GetUserId()!.Value;

            viewModel.AvatarUrl = _loadUserRepositryReal.GetAvatarUrl(userId);

            viewModel.LoadValumes = _loadVolumeTestingRepositoryReal
                            .GetLoadValueWithInfoAboutAuthors(userId)
                            .Select(x => new LoadVolumeShortInfoViewModel
                            {
                                Name = x.Name,
                                IsCreatedWithСharacter = x.HasCharaterWithSpecialAuthor
                            })
                            .ToList();

            viewModel.Metrics = _loadTestingRepository
                .GetAllByAuthorId(userId)
                .Select(x => new MetricShortInfoViewModel
                {
                    Name = x.Name,
                    Throughput = x.Throughput,
                    Average = x.Average

                })
                .ToList();

            return View(viewModel);
        }


        public IActionResult UpdateNameById(int id, string newName)
        {
            _loadTestingRepository.UpdateNameById(id, newName);
            return RedirectToAction("ContenMetricsListView");
        }


        public IActionResult UpdateNameByGuid(Guid Guid, string newName)
        {
            _loadTestingRepository.UpdateNameByGuid(Guid, newName);
            return RedirectToAction("ContenMetricsListView");
        }


        public IActionResult UpdateThroughputById(int Id, decimal Throughput)
        {
            _loadTestingRepository.UpdateThroughputById(Id, Throughput);
            return RedirectToAction("ContenMetricsListView");
        }

        public IActionResult UpdateThroughputByGuid(Guid Guid, decimal Throughput)
        {
            _loadTestingRepository.UpdateThroughputByGuid(Guid, Throughput);
            return RedirectToAction("ContenMetricsListView");
        }

        public IActionResult UpdateMetric(MetricCreationViewModel metric)
        {
            _loadTestingRepository.UpdateNameByGuid(metric.Guid, metric.Name);
            _loadTestingRepository.UpdateThroughputByGuid(metric.Guid, (decimal)metric.Throughput);
            _loadTestingRepository.UpdateAverageByGuid(metric.Guid, (decimal)metric.Average);

            return RedirectToAction("ContenMetricsListView");
            /*
              <input type="hidden" name="id" value="@metric.Id" />
              <input type="text" name="name" value="@metric.Name" />
              <input type="hidden" name="guid" value="@metric.Guid" />
              <input type="text" name="throughput" value="@metric.Throughput" />
              <input type="text" name="average" value="@metric.Average" />*/
        }

        public IActionResult RemoveById(int id)
        {
            _loadTestingRepository.Delete(id);
            return RedirectToAction("ContenMetricsListView");
        }

        public IActionResult RemoveByGuid(Guid Guid)
        {
            _loadTestingRepository.DeleteByGuid(Guid);
            return RedirectToAction("ContenMetricsListView");
        }


    }
}
