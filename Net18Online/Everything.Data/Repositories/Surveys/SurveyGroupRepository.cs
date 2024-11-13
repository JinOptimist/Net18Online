using Everything.Data.Interface.Repositories;
using Everything.Data.Models.Surveys;

namespace Everything.Data.Repositories.Surveys
{
    public interface ISurveyGroupRepositoryReal : ISurveyGroupRepository<SurveyGroupData>
    {
    }

    public class SurveyGroupRepository : BaseRepository<SurveyGroupData>, ISurveyGroupRepositoryReal
    {
        public SurveyGroupRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public void UpdateTitle(int id, string newTitle)
        {
            var surveyGroup = _webDbContext
                .SurveyGroups
                .First(x => x.Id == id);

            surveyGroup.Title = newTitle;

            _webDbContext.SaveChanges();
        }

        public bool HasUniqueTitle(string title)
        {
            return !_dbSet.Any(x => x.Title == title);
        }
    }
}
