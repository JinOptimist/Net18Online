using Everything.Data.Interface.Repositories;
using Everything.Data.Models.Surveys;

namespace Everything.Data.Repositories.Surveys
{
    public interface ISurveysRepositoryReal : ISurveysRepository<SurveyData>
    {
    }

    public class SurveysRepository : BaseRepository<SurveyData>, ISurveysRepositoryReal
    {
        public SurveysRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public void CreateSurvey(string title, int groupId, string? description)
        {
            var group = _webDbContext
                .SurveyGroups
                .First(x => x.Id == groupId);

            var survey = new SurveyData()
            {
                SurveyGroup = group,
                IdStatus = 1, // Пока такой хардкод, пока не сделана связка между таблицами
                Title = title,
                Description = description
            };

            _dbSet.Add(survey);
            _webDbContext.SaveChanges();
        }

        public void UpdateTitle(int id, string newTitle)
        {
            var surveyGroup = _webDbContext
                .Surveys
                .First(x => x.Id == id);

            surveyGroup.Title = newTitle;

            _webDbContext.SaveChanges();
        }
    }
}
