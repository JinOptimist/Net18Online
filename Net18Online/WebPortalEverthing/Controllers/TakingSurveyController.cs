using Everything.Data.Interface.Repositories;
using Everything.Data.Repositories.Surveys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.Surveys;
using WebPortalEverthing.Services;

namespace WebPortalEverthing.Controllers
{
    [Authorize]
    public class TakingSurveyController : Controller
    {
        private ISurveysRepositoryReal _surveysRepository;
        private IQuestionRepositoryReal _questionRepository;
        private ITakingUserSurveyRepositoryReal _takingUserSurveyRepository;
        private AuthService _authService;

        public TakingSurveyController(ISurveysRepositoryReal surveysRepository, AuthService authService, IQuestionRepositoryReal questionRepository, ITakingUserSurveyRepositoryReal takingUserSurveyRepository)
        {
            _surveysRepository = surveysRepository;
            _authService = authService;
            _questionRepository = questionRepository;
            _takingUserSurveyRepository = takingUserSurveyRepository;
        }

        public ActionResult Index(int surveyId)
        {
            var userId = _authService.GetUserId()!.Value;

            var takingId = _takingUserSurveyRepository.Add(userId, surveyId);

            var survey = _surveysRepository.Get(surveyId);
            var questions = _questionRepository.GetQuestionsForSurvey(surveyId);

            var viewModel = new TakingSurveyIndexViewModel
            {
                Id = takingId,
                SurveyId = surveyId,
                SurveyTitle = survey.Title,
                Questions = questions
                    .Select(question => new QuestionViewModel
                    {
                        Id = question.Id,
                        Title = question.Title,
                        IsRequired = question.IsRequired,
                        AnswerType = question.AnswerType
                    })
                    .ToList()
            };

            return View(viewModel);
        }
    }
}