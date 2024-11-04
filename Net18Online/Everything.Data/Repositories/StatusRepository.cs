using Everything.Data.Interface.Repositories;
using Everything.Data.Models.Surveys;

namespace Everything.Data.Repositories
{
    public interface IStatusRepositoryReal : IStatusRepository<StatusData>
    {
    }

    public class StatusRepository : IStatusRepositoryReal
    {
        private WebDbContext _webDbContext;

        public StatusRepository(WebDbContext webDbContext)
        {
            _webDbContext = webDbContext;
        }

        public void Add(StatusData data)
        {
            _webDbContext.Add(data);
            _webDbContext.SaveChanges();
        }

        public bool Any()
        {
            return _webDbContext.SurveyGroups.Any();
        }

        public void Delete(StatusData data)
        {
            _webDbContext.Statuses.Remove(data);
            _webDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = Get(id);
            Delete(data);
        }

        public StatusData? Get(int id)
        {
            return _webDbContext
                .Statuses
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<StatusData> GetAll()
        {
            return _webDbContext
                .Statuses
                .ToList();
        }

        public void UpdateTitle(int id, string newTitle)
        {
            var surveyGroup = _webDbContext
                .Statuses
                .First(x => x.Id == id);

            surveyGroup.Title = newTitle;

            _webDbContext.SaveChanges();
        }
    }
}
