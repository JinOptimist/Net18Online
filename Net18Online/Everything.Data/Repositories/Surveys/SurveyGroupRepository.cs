using Everything.Data.Interface.Repositories;
using Everything.Data.Models.Surveys;
using Microsoft.EntityFrameworkCore;

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

        public IEnumerable<SurveyGroupData> GetAllWithСreatorUsers()
        {
            return _dbSet
                .Include(x => x.СreatorUser)
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

        public bool HasUniqueTitle(string title)
        {
            return !_dbSet.Any(x => x.Title == title);
        }

        public void CreateSurveyGroup(string title, int? userId)
        {
            var user = _webDbContext
                .Users
                .FirstOrDefault(x => x.Id == userId);

            var surveyGroup = new SurveyGroupData()
            {
                Title = title,
                СreatorUser = user
            };

            Add(surveyGroup);
        }
    }
}
