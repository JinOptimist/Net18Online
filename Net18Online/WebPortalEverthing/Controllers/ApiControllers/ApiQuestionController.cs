using Everything.Data.Interface.Enums;
using Everything.Data.Repositories.Surveys;
using Microsoft.AspNetCore.Mvc;

namespace WebPortalEverthing.Controllers.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiQuestionController : ControllerBase
    {
        private IQuestionRepositoryReal _questionRepository;

        public ApiQuestionController(IQuestionRepositoryReal questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public int Create(int surveyId)
        {
            return _questionRepository.Create(surveyId);
        }

        public bool UpdateTitle(int id, string value)
        {
            _questionRepository.UpdateTitle(id, value);
            return true;
        }

        public bool UpdateRequired(int id, bool value)
        {
            _questionRepository.UpdateRequired(id, value);
            return true;
        }

        public bool UpdateAnswerType(int id, AnswerType value)
        {
            _questionRepository.UpdateAnswerType(id, value);
            return true;
        }

        public bool Delete(int id)
        {
            _questionRepository.Delete(id);
            return true;
        }
    }
}
