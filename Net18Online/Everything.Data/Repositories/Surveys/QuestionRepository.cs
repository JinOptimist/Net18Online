using Everything.Data.Interface.Enums;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models.Surveys;

namespace Everything.Data.Repositories.Surveys
{
    public interface IQuestionRepositoryReal : IQuestionRepository<QuestionData>
    {
    }

    public class QuestionRepository : BaseRepository<QuestionData>, IQuestionRepositoryReal
    {
        public QuestionRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public int Create(int surveyId)
        {
            var survey = _webDbContext.Surveys.First(x => x.Id == surveyId);

            var data = new QuestionData()
            {
                Title = string.Empty,
                IsRequired = false,
                AnswerType = AnswerType.TextString,
                Survey = survey
            };

            return Add(data);
        }

        public void UpdateTitle(int id, string value)
        {
            var question = Get(id);

            question.Title = value;

            _webDbContext.SaveChanges();
        }

        public void UpdateRequired(int id, bool value)
        {
            var question = Get(id);

            question.IsRequired = value;

            _webDbContext.SaveChanges();
        }

        public void UpdateAnswerType(int id, AnswerType value)
        {
            var question = Get(id);

            question.AnswerType = value;

            _webDbContext.SaveChanges();
        }
    }
}
