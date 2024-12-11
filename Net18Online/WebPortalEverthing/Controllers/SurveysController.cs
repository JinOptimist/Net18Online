﻿using Everything.Data.Models.Surveys;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.Surveys;
using Everything.Data.Interface.Models.Surveys;
using Everything.Data.Repositories.Surveys;
using Everything.Data.Interface.Enums;
using Enums.Users;
using WebPortalEverthing.Controllers.AuthAttributes;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Controllers
{
    public class SurveysController : Controller
    {
        private ISurveyGroupRepositoryReal _surveyGroupRepository;
        private IStatusRepositoryReal _statusRepository;
        private ISurveysRepositoryReal _surveysRepository;
        private AuthService _authService;

        public SurveysController(ISurveyGroupRepositoryReal surveyGroupRepository, IStatusRepositoryReal statusRepository, ISurveysRepositoryReal surveysRepository, AuthService authService)
        {
            _statusRepository = statusRepository;
            _surveyGroupRepository = surveyGroupRepository;
            _surveysRepository = surveysRepository;
            _authService = authService;
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

            var surveyExpandGroups = HttpContext.Request.Cookies["survey-expand-groups"];
            var isExpandGroups = false;
            bool.TryParse(surveyExpandGroups, out isExpandGroups);

            var viewModel = new SurveysAllViewModel()
            {
                SurveyGroups = surveyGroupsViewModels,
                IsExpandGroups = isExpandGroups
            };

            return View(viewModel);
        }

        private SurveyGroupViewModel GetSurveyGroupViewModelFromData(ISurveyGroupData surveyGroup)
        {
            var isAllowSurveyCreation = _authService.HasRole(Role.SurveysCreatorOrEditor);
            var surveysFromDb = _surveysRepository.GetAll();

            return new SurveyGroupViewModel
            {
                Id = surveyGroup.Id,
                Title = surveyGroup.Title,
                IsAllowSurveyCreation = isAllowSurveyCreation,
                Surveys = surveysFromDb
                            .Where(survey => survey.SurveyGroup.Id == surveyGroup.Id)
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
            var surveyGroup = _surveyGroupRepository
                .Get(1);

            var survey = new SurveyData()
            {
                SurveyGroup = surveyGroup,
                IdStatus = 2,
                Title = "Самооценка сотрудника"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                SurveyGroup = surveyGroup,
                IdStatus = 2,
                Title = "Карьерные ожидания сотрудников"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                SurveyGroup = surveyGroup,
                IdStatus = 2,
                Title = "Диагностики синдрома выгорания"
            };
            _surveysRepository.Add(survey);

            surveyGroup = _surveyGroupRepository
                .Get(2);

            survey = new SurveyData()
            {
                SurveyGroup = surveyGroup,
                IdStatus = 2,
                Title = "Анкета удовлетворенности сотрудников"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                SurveyGroup = surveyGroup,
                IdStatus = 2,
                Title = "Удовлетворенность работой и вознаграждениями"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                SurveyGroup = surveyGroup,
                IdStatus = 2,
                Title = "Удовлетворенность условиями труда с оценкой важности"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                SurveyGroup = surveyGroup,
                IdStatus = 1,
                Title = "Название опроса 6"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                SurveyGroup = surveyGroup,
                IdStatus = 1,
                Title = "Название опроса 7"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                SurveyGroup = surveyGroup,
                IdStatus = 1,
                Title = "Название опроса 8"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                SurveyGroup = surveyGroup,
                IdStatus = 1,
                Title = "Название опроса 9"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                SurveyGroup = surveyGroup,
                IdStatus = 1,
                Title = "Название опроса 10"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                SurveyGroup = surveyGroup,
                IdStatus = 1,
                Title = "Название опроса 11"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                SurveyGroup = surveyGroup,
                IdStatus = 1,
                Title = "Название опроса 12"
            };
            _surveysRepository.Add(survey);

            surveyGroup = _surveyGroupRepository
                .Get(3);

            survey = new SurveyData()
            {
                SurveyGroup = surveyGroup,
                IdStatus = 1,
                Title = "Корпоративная культура"
            };
            _surveysRepository.Add(survey);

            survey = new SurveyData()
            {
                SurveyGroup = surveyGroup,
                IdStatus = 2,
                Title = "Диагностика аврала"
            };
            _surveysRepository.Add(survey);
        }

        [HttpGet]
        [HasRole(Role.SurveysCreatorOrEditor)]
        public ActionResult Create(int idGroup)
        {
            var surveyGroup = _surveyGroupRepository
                .Get(idGroup);

            var surveyCreate = new SurveyCreateViewModel()
            {
                Id = 0,
                SurveyGroup = new SurveyGroupForListViewModel()
                {
                    Id = surveyGroup.Id,
                    Title = surveyGroup.Title,
                },
                Questions = new()
                {
                    new QuestionViewModel
                    {
                        Title = "Вопрос 1",
                        IsRequired = true,
                        AnswerType = AnswerType.TextString
                    },
                    new QuestionViewModel
                    {
                        Title = "Вопрос 2",
                        IsRequired = true,
                        AnswerType = AnswerType.TextString
                    }
                }
            };

            return View(surveyCreate);
        }

        [HttpPost]
        [HasRole(Role.SurveysCreatorOrEditor)]
        public ActionResult Create(SurveyCreateViewModel surveyCreate)
        {
            if (!ModelState.IsValid)
            {
                return View(surveyCreate);
            }

            _surveysRepository.CreateSurvey(surveyCreate.Title, surveyCreate.SurveyGroup.Id, surveyCreate.Description);

            return RedirectToAction(nameof(SurveysAll));
        }

        [HttpGet]
        [HasRole(Role.SurveysCreatorOrEditor)]
        public ActionResult Edit(int idSurvey)
        {
            var surveyFromDb = _surveysRepository.GetWithGroupAndQuestions(idSurvey);

            var surveyCreate = new SurveyCreateViewModel()
            {
                Id = idSurvey,
                Title = surveyFromDb.Title,
                Description = surveyFromDb.Description,
                SurveyGroup = new SurveyGroupForListViewModel()
                {
                    Id = surveyFromDb.SurveyGroup.Id,
                    Title = surveyFromDb.SurveyGroup.Title,
                },
                Questions = surveyFromDb
                    .Questions
                    .Select(question => new QuestionViewModel
                    {
                        Id = question.Id,
                        Title = question.Title,
                        IsRequired = question.IsRequired,
                        AnswerType = question.AnswerType
                    })
                    .ToList()
            };

            return View(nameof(Create), surveyCreate);
        }

        [HttpPost]
        [HasRole(Role.SurveysCreatorOrEditor)]
        public ActionResult Edit(SurveyCreateViewModel surveyCreate)
        {
            if (!ModelState.IsValid)
            {
                return View(surveyCreate);
            }

            _surveysRepository.UpdateTitle(surveyCreate.Id, surveyCreate.Title);
            _surveysRepository.UpdateDescription(surveyCreate.Id, surveyCreate.Description);

            return RedirectToAction(nameof(SurveysAll));
        }
    }
}
