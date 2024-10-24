using Everything.Data.Interface.Repositories;
using Everything.Data.Fake.Models.Surveys;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.Surveys;
using Everything.Data.Interface.Models.Surveys;

namespace WebPortalEverthing.Controllers
{
    public class SurveysController : Controller
    {
        private ISurveysRepository _surveysRepository;

        public SurveysController(ISurveysRepository surveysRepository)
        {
            _surveysRepository = surveysRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SurveysAll()
        {
            if (!_surveysRepository.AnySurveyGroups())
            {
                GenerateDefaultStatuses();
                GenerateDefaultGroups();
                GenerateDefaultSurveysInGroups();
            }

            var groupsFromDb = _surveysRepository.GetAllSurveyGroups();
            var surveysFromDb = _surveysRepository.GetAllSurveys();

            var surveyGroupsViewModels = groupsFromDb
                .Select(dbGroup =>
                    new SurveyGroupViewModel
                    {
                        Id = dbGroup.Id,
                        Title = dbGroup.Title,
                        Surveys = surveysFromDb
                            .Where(survey => survey.IdGroup == dbGroup.Id)
                            .Select(dbSurvey => GetSurveyViewModelFromData(dbSurvey))
                            .ToList(),
                    }
                )
                .ToList();

            return View(surveyGroupsViewModels);
        }

        private SurveyViewModel GetSurveyViewModelFromData(ISurvey survey)
        {
            return new SurveyViewModel
            {
                Id = survey.Id,
                Status = GetSurveyStatusViewModelFromData(survey),
                Title = survey.Title,
                Action = GetSurveyActionModelFromData(survey)
            };
        }

        private SurveyStatusViewModel GetSurveyStatusViewModelFromData(ISurvey survey)
        {
            var statusesFromDb = _surveysRepository.GetAllStatuses();
            var statusFromDb = statusesFromDb
                .Where(i => i.Id == survey.IdStatus)
                .FirstOrDefault();

            return new SurveyStatusViewModel()
            {
                Title = statusFromDb.Title,
                ImagesSrc = statusFromDb.ImagesSrc
            };
        }

        private SurveyActionViewModel? GetSurveyActionModelFromData(ISurvey survey)
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
                Id = 0,
                Title = "Новый опрос",
                ImagesSrc = "/images/Surveys/status/new-48.png",
            };
            _surveysRepository.AddStatus(statusNew);

            var statusCompleted = new StatusData()
            {
                Id = 1,
                Title = "Опрос пройден",
                ImagesSrc = "/images/Surveys/status/status-50.png",
            };
            _surveysRepository.AddStatus(statusCompleted);
        }

        private void GenerateDefaultGroups()
        {
            var group = new SurveyGroupData()
            {
                Title = "Оценка / самооценка"
            };
            _surveysRepository.AddSurveyGroup(group);

            group = new SurveyGroupData()
            {
                Title = "Удовлетворенность"
            };
            _surveysRepository.AddSurveyGroup(group);

            group = new SurveyGroupData()
            {
                Title = "Прочее"
            };
            _surveysRepository.AddSurveyGroup(group);
        }

        private void GenerateDefaultSurveysInGroups()
        {
            var survey = new SurveyData()
            {
                IdGroup = 1,
                IdStatus = 1,
                Title = "Самооценка сотрудника"
            };
            _surveysRepository.AddSurvey(survey);

            survey = new SurveyData()
            {
                IdGroup = 1,
                IdStatus = 1,
                Title = "Карьерные ожидания сотрудников"
            };
            _surveysRepository.AddSurvey(survey);

            survey = new SurveyData()
            {
                IdGroup = 1,
                IdStatus = 1,
                Title = "Диагностики синдрома выгорания"
            };
            _surveysRepository.AddSurvey(survey);

            survey = new SurveyData()
            {
                IdGroup = 2,
                IdStatus = 1,
                Title = "Анкета удовлетворенности сотрудников"
            };
            _surveysRepository.AddSurvey(survey);

            survey = new SurveyData()
            {
                IdGroup = 2,
                IdStatus = 1,
                Title = "Удовлетворенность работой и вознаграждениями"
            };
            _surveysRepository.AddSurvey(survey);

            survey = new SurveyData()
            {
                IdGroup = 2,
                IdStatus = 1,
                Title = "Удовлетворенность условиями труда с оценкой важности"
            };
            _surveysRepository.AddSurvey(survey);

            survey = new SurveyData()
            {
                IdGroup = 2,
                IdStatus = 0,
                Title = "Название опроса 6"
            };
            _surveysRepository.AddSurvey(survey);

            survey = new SurveyData()
            {
                IdGroup = 2,
                IdStatus = 0,
                Title = "Название опроса 7"
            };
            _surveysRepository.AddSurvey(survey);

            survey = new SurveyData()
            {
                IdGroup = 2,
                IdStatus = 0,
                Title = "Название опроса 8"
            };
            _surveysRepository.AddSurvey(survey);

            survey = new SurveyData()
            {
                IdGroup = 2,
                IdStatus = 0,
                Title = "Название опроса 9"
            };
            _surveysRepository.AddSurvey(survey);

            survey = new SurveyData()
            {
                IdGroup = 2,
                IdStatus = 0,
                Title = "Название опроса 10"
            };
            _surveysRepository.AddSurvey(survey);

            survey = new SurveyData()
            {
                IdGroup = 2,
                IdStatus = 0,
                Title = "Название опроса 11"
            };
            _surveysRepository.AddSurvey(survey);

            survey = new SurveyData()
            {
                IdGroup = 2,
                IdStatus = 0,
                Title = "Название опроса 12"
            };
            _surveysRepository.AddSurvey(survey);

            survey = new SurveyData()
            {
                IdGroup = 3,
                IdStatus = 0,
                Title = "Корпоративная культура"
            };
            _surveysRepository.AddSurvey(survey);

            survey = new SurveyData()
            {
                IdGroup = 3,
                IdStatus = 1,
                Title = "Диагностика аврала"
            };
            _surveysRepository.AddSurvey(survey);
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
                IdStatus = 0,
                Title = surveyCreate.Title
            };
            _surveysRepository.AddSurvey(survey);

            return RedirectToAction(nameof(SurveysAll));
        }
    }
}
