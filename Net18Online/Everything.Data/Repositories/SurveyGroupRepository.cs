using Everything.Data.Interface.Repositories;
using Everything.Data.Models.Surveys;

namespace Everything.Data.Repositories
{
    public interface ISurveyGroupRepositoryReal : ISurveyGroupRepository<SurveyGroupData>
    {
    }

    public class SurveyGroupRepository : ISurveyGroupRepositoryReal
    {
        private WebDbContext _webDbContext;

        public SurveyGroupRepository(WebDbContext webDbContext)
        {
            _webDbContext = webDbContext;
        }

        public void Add(SurveyGroupData data)
        {
            _webDbContext.Add(data);
            _webDbContext.SaveChanges();
        }

        public bool Any()
        {
            return _webDbContext.SurveyGroups.Any();
        }

        public void Delete(SurveyGroupData data)
        {
            _webDbContext.SurveyGroups.Remove(data);
            _webDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = Get(id);
            Delete(data);
        }

        public SurveyGroupData? Get(int id)
        {
            return _webDbContext
                .SurveyGroups
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<SurveyGroupData> GetAll()
        {
            return _webDbContext
                .SurveyGroups
                .ToList();
        }

        public void UpdateTitle(int id, string newTitle)
        {
            var surveyGroup = _webDbContext
                .SurveyGroups
                .First(x => x.Id == id);

            surveyGroup.Title = newTitle;

            _webDbContext.SaveChanges();
        }
    }
}
