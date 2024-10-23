using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.Surveys;

namespace WebPortalEverthing.Controllers
{
    public class SurveysController : Controller
    {
        // BAD. DO NOT USE THIS ON PROD
        private static List<SurveyGroupViewModel> SurveyGroups = new();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SurveysAll()
        {
            if (!SurveyGroups.Any())
            {
                var statusNew = new SurveyStatusViewModel()
                {
                    Title = "Новый опрос",
                    ImagesSrc = "/images/Surveys/status/new-48.png",
                };

                var statusCompleted = new SurveyStatusViewModel()
                {
                    Title = "Опрос пройден",
                    ImagesSrc = "/images/Surveys/status/status-50.png",
                };

                var buttonTakeSurvey = new SurveyActionViewModel()
                {
                    Title = "Пройти"
                };

                var group1 = new List<SurveyViewModel>
                {
                    new SurveyViewModel()
                    {
                        Status = statusCompleted,
                        Title = "Самооценка сотрудника"
                    },
                    new SurveyViewModel()
                    {
                        Status = statusCompleted,
                        Title = "Карьерные ожидания сотрудников"
                    },
                    new SurveyViewModel()
                    {
                        Status = statusNew,
                        Title = "Диагностики синдрома выгорания",
                        Action = buttonTakeSurvey
                    }
                };

                SurveyGroups.Add(new()
                {
                    Title = "Оценка / самооценка",
                    Surveys = group1
                });

                var group2 = new List<SurveyViewModel>
                {
                    new SurveyViewModel()
                    {
                        Status = statusCompleted,
                        Title = "Анкета удовлетворенности сотрудников"
                    },
                    new SurveyViewModel()
                    {
                        Status = statusCompleted,
                        Title = "Удовлетворенность работой и вознаграждениями"
                    },
                    new SurveyViewModel()
                    {
                        Status = statusCompleted,
                        Title = "Удовлетворенность условиями труда с оценкой важности"
                    },
                    new SurveyViewModel()
                    {
                        Status = statusNew,
                        Title = "Название опроса 6",
                        Action = buttonTakeSurvey
                    },
                    new SurveyViewModel()
                    {
                        Status = statusNew,
                        Title = "Название опроса 7",
                        Action = buttonTakeSurvey
                    },
                    new SurveyViewModel()
                    {
                        Status = statusNew,
                        Title = "Название опроса 8",
                        Action = buttonTakeSurvey
                    },
                    new SurveyViewModel()
                    {
                        Status = statusNew,
                        Title = "Название опроса 9",
                        Action = buttonTakeSurvey
                    },
                    new SurveyViewModel()
                    {
                        Status = statusNew,
                        Title = "Название опроса 10",
                        Action = buttonTakeSurvey
                    },
                    new SurveyViewModel()
                    {
                        Status = statusNew,
                        Title = "Название опроса 11",
                        Action = buttonTakeSurvey
                    },
                    new SurveyViewModel()
                    {
                        Status = statusNew,
                        Title = "Название опроса 12",
                        Action = buttonTakeSurvey
                    }
                };

                SurveyGroups.Add(new()
                {
                    Title = "Удовлетворенность",
                    Surveys = group2
                });

                var group3 = new List<SurveyViewModel>
                {
                    new SurveyViewModel()
                    {
                        Status = statusNew,
                        Title = "Корпоративная культура",
                        Action = buttonTakeSurvey
                    },
                    new SurveyViewModel()
                    {
                        Status = statusCompleted,
                        Title = "Диагностика аврала"
                    }
                };

                SurveyGroups.Add(new()
                {
                    Title = "Прочее",
                    Surveys = group3
                });
            }

            return View(SurveyGroups);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SurveyCreateViewModel surveyCreate)
        {
            var statusNew = new SurveyStatusViewModel()
            {
                Title = "Новый опрос",
                ImagesSrc = "/images/Surveys/status/new-48.png",
            };

            var buttonTakeSurvey = new SurveyActionViewModel()
            {
                Title = "Пройти"
            };

            var firstGroup = SurveyGroups.First();

            firstGroup.Surveys.Add(new()
            {
                Status = statusNew,
                Title = surveyCreate.Title,
                Action = buttonTakeSurvey
            });

            return RedirectToAction(nameof(SurveysAll));
        }
    }
}
