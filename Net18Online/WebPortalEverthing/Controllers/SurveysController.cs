using Everything.Data.Models.Surveys;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.Surveys;
using Everything.Data.Interface.Models.Surveys;
using Everything.Data.Repositories;

namespace WebPortalEverthing.Controllers
{
    public class SurveysController : Controller
    {
        private ISurveyGroupRepositoryReal _surveyGroupRepository;
        private IStatusRepositoryReal _statusRepository;
        private ISurveysRepositoryReal _surveysRepository;

        public SurveysController(ISurveyGroupRepositoryReal surveyGroupRepository, IStatusRepositoryReal statusRepository, ISurveysRepositoryReal surveysRepository)
        {
            _statusRepository = statusRepository;
            _surveyGroupRepository = surveyGroupRepository;
            _surveysRepository = surveysRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SurveysAll()
        {
            if (!_statusRepository.Any())
            {
                GenerateDefaultStatuses();
            }

            if (!_surveyGroupRepository.Any())
            {
                GenerateDefaultGroups();
            }

            if (!_surveysRepository.Any())
            {
                GenerateDefaultSurveysInGroups();
            }

            var groupsFromDb = _surveyGroupRepository.GetAll();

            var surveyGroupsViewModels = groupsFromDb
                .Select(GetSurveyGroupViewModelFromData)
                .ToList();

            return View(surveyGroupsViewModels);
        }

        private SurveyGroupViewModel GetSurveyGroupViewModelFromData(ISurveyGroupData surveyGroup)
        {
            var surveysFromDb = _surveysRepository.GetAll();

            return new SurveyGroupViewModel
            {
                Id = surveyGroup.Id,
                Title = surveyGroup.Title,
                Surveys = surveysFromDb
                            .Where(survey => survey.IdGroup == surveyGroup.Id)
                            .Select(GetSurveyViewModelFromData)
                            .ToList(),
            };
        }

        private SurveyViewModel GetSurveyViewModelFromData(ISurveyData survey)
        {
            return new SurveyViewModel
            {
                Id = survey.Id,
                Status = GetSurveyStatusViewModelFromData(survey),
                Title = survey.Title,
                Action = GetSurveyActionModelFromData(survey)
            };
        }

        private SurveyStatusViewModel GetSurveyStatusViewModelFromData(ISurveyData survey)
        {
            var statusesFromDb = _statusRepository.GetAll();
            var statusFromDb = statusesFromDb
                .Where(i => i.Id == survey.IdStatus)
                .FirstOrDefault();

            return new SurveyStatusViewModel()
            {
                Title = statusFromDb.Title,
                ImagesSrc = statusFromDb.ImagesSrc
            };
        }

        private SurveyActionViewModel? GetSurveyActionModelFromData(ISurveyData survey)
        {
            // Пока что придумана одна кнопка, в будующем будет несколько
            var buttonTakeSurvey = new SurveyActionViewModel()
            {
                Title = "Пройти"
            };

            return survey.IdStatus switch
            {
                0 => buttonTakeSurvey,
                _ => null
            };
        }

        private void GenerateDefaultStatuses()
        {
            var statusNew = new StatusData()
            {
                Title = "Новый опрос",
                ImagesSrc = "/images/Surveys/status/new-48.png",
            };
            _statusRepository.Add(statusNew);

            var statusCompleted = new StatusData()
            {
                Title = "Опрос пройден",
                ImagesSrc = "/images/Surveys/status/status-50.png",
            };
            _statusRepository.Add(statusCompleted);
        }

        private void GenerateDefaultGroups()
        {
            var group = new SurveyGroupData()
            {
                Title = "Оценка / самооценка"
            };
            _surveyGroupRepository.Add(group);

            group = new SurveyGroupData()
            {
                Title = "Удовлетворенность"
            };
            _surveyGroupRepository.Add(group);

            group = new SurveyGroupData()
            {
                Title = "Прочее"
            };
            _surveyGroupRepository.Add(group);
        }

        private void GenerateDefaultSurveysInGroups()
        {
            var survey = new SurveyData()
            {
                IdGroup = 1,
                IdStatus = 2,
                Title = "Самооценка сотрудника"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                IdGroup = 1,
                IdStatus = 2,
                Title = "Карьерные ожидания сотрудников"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                IdGroup = 1,
                IdStatus = 2,
                Title = "Диагностики синдрома выгорания"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                IdGroup = 2,
                IdStatus = 2,
                Title = "Анкета удовлетворенности сотрудников"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                IdGroup = 2,
                IdStatus = 2,
                Title = "Удовлетворенность работой и вознаграждениями"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                IdGroup = 2,
                IdStatus = 2,
                Title = "Удовлетворенность условиями труда с оценкой важности"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                IdGroup = 2,
                IdStatus = 1,
                Title = "Название опроса 6"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                IdGroup = 2,
                IdStatus = 1,
                Title = "Название опроса 7"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                IdGroup = 2,
                IdStatus = 1,
                Title = "Название опроса 8"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                IdGroup = 2,
                IdStatus = 1,
                Title = "Название опроса 9"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                IdGroup = 2,
                IdStatus = 1,
                Title = "Название опроса 10"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                IdGroup = 2,
                IdStatus = 1,
                Title = "Название опроса 11"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                IdGroup = 2,
                IdStatus = 1,
                Title = "Название опроса 12"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                IdGroup = 3,
                IdStatus = 1,
                Title = "Корпоративная культура"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                IdGroup = 3,
                IdStatus = 2,
                Title = "Диагностика аврала"
            };
            _surveysRepository.Add(survey);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SurveyCreateViewModel surveyCreate)
        {
            var survey = new SurveyData()
            {
                IdGroup = 1, // Пока что харкод, пока не умеем пробрасывать
                IdStatus = 1,
                Title = surveyCreate.Title
            };
            _surveysRepository.Add(survey);

            return RedirectToAction(nameof(SurveysAll));
        }
    }
}
