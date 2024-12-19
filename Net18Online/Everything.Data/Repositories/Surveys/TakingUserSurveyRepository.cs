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

        public int? ReturnIdLastUncompletedSurvey(int userId)
        {
            if (!_dbSet.Any())
            {
                return null;
            }

            return _dbSet
                .Where(x => x.User.Id == userId
                    && x.CompletionStatus == SurveyCompletionStatus.InProgress)
                .OrderByDescending(x => x.StartTime)
                .Take(1)
                .First()
                .Id;
        }

        public int Add(int userId, int surveyId)
        {
            var user = _webDbContext.Users.First(u => u.Id == userId);
            var survey = _webDbContext.Surveys.First(s => s.Id == surveyId);

            var takingData = new TakingUserSurveyData()
            {
                StartTime = DateTime.Now,
                CompletionStatus = SurveyCompletionStatus.InProgress,
                User = user,
                Survey = survey
            };

            _dbSet.Add(takingData);
            _webDbContext.SaveChanges();

            // Создадим заглушки для ответов
            var questions = _webDbContext.Questions.Where(q => q.Survey.Id == surveyId);
            var answerData = questions.Select(q => new AnswerToQuestionData
            {
                Text = string.Empty,
                TakingUserSurvey = takingData,
                Question = q
            });

            _webDbContext.AnswerToQuestions.AddRange(answerData);
            _webDbContext.SaveChanges();

            return takingData.Id;
        }
    }
}
