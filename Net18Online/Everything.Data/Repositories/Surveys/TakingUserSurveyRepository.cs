using Enums.Surveys;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models.Surveys;

namespace Everything.Data.Repositories.Surveys
{
    public interface ITakingUserSurveyRepositoryReal : ITakingUserSurveyRepository<TakingUserSurveyData>
    {
    }

    public class TakingUserSurveyRepository : BaseRepository<TakingUserSurveyData>, ITakingUserSurveyRepositoryReal
    {
        public TakingUserSurveyRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public override int Add(TakingUserSurveyData data)
        {
            throw new NotImplementedException($"Use method {nameof(Add)} with parameters userId and surveyId to add a record");
        }

        public int Add(int userId, int surveyId)
        {
            var user = _webDbContext.Users.First(u => u.Id == userId);
            var survey = _webDbContext.Surveys.First(s => s.Id == surveyId);

            var data = new TakingUserSurveyData()
            {
                StartTime = DateTime.Now,
                CompletionStatus = SurveyCompletionStatus.InProgress,
                User = user,
                Survey = survey
            };

            _dbSet.Add(data);
            _webDbContext.SaveChanges();

            return data.Id;
        }
    }
}
