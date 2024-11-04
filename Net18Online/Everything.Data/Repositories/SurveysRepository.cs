using Everything.Data.Interface.Repositories;
using Everything.Data.Models.Surveys;

namespace Everything.Data.Repositories
{
    public interface ISurveysRepositoryReal : ISurveysRepository<SurveyData>
    {
    }

    public class SurveysRepository : ISurveysRepositoryReal
    {
        private WebDbContext _webDbContext;

        public SurveysRepository(WebDbContext webDbContext)
        {
            _webDbContext = webDbContext;
        }

        public void Add(SurveyData data)
        {
            _webDbContext.Add(data);
            _webDbContext.SaveChanges();
        }

        public bool Any()
        {
            return _webDbContext.Surveys.Any();
        }

        public void Delete(SurveyData data)
        {
            _webDbContext.Surveys.Remove(data);
            _webDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = Get(id);
            Delete(data);
        }

        public SurveyData? Get(int id)
        {
            return _webDbContext
                .Surveys
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<SurveyData> GetAll()
        {
            return _webDbContext
                .Surveys
                .ToList();
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
