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
