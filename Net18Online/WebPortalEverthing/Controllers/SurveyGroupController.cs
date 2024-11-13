using Everything.Data.Models.Surveys;
using Microsoft.AspNetCore.Mvc;
using WebPortalEverthing.Models.Surveys;
using Everything.Data.Interface.Models.Surveys;
using Everything.Data.Repositories.Surveys;
using WebPortalEverthing.Models.AnimeGirl;

namespace WebPortalEverthing.Controllers
{
    public class SurveyGroupController : Controller
    {
        private ISurveyGroupRepositoryReal _surveyGroupRepository;
        private IStatusRepositoryReal _statusRepository;

        public SurveyGroupController(ISurveyGroupRepositoryReal surveyGroupRepository, IStatusRepositoryReal statusRepository)
        {
            _statusRepository = statusRepository;
            _surveyGroupRepository = surveyGroupRepository;
        }

        public ActionResult Index()
        {
            var groupsFromDb = _surveyGroupRepository.GetAll();

            var surveyGroupsViewModels = groupsFromDb
                .Select(GetSurveyGroupViewModelFromData)
                .ToList();

            return View(surveyGroupsViewModels);
        }

        private SurveyGroupViewModel GetSurveyGroupViewModelFromData(ISurveyGroupData surveyGroup)
        {
            return new SurveyGroupViewModel
            {
                Id = surveyGroup.Id,
                Title = surveyGroup.Title
            };
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SurveyGroupCreateViewModel viewModel)
        {
            if (_surveyGroupRepository.HasSimilarTitle(viewModel.Title))
            {
                ModelState.AddModelError(
                    nameof(SurveyGroupCreateViewModel.Title),
                    "Группа опросов с таким названием уже существует");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var surveyGroup = new SurveyGroupData()
            {
                Title = viewModel.Title
            };
            _surveyGroupRepository.Add(surveyGroup);

            return RedirectToAction(nameof(Index));
        }
    }
}
