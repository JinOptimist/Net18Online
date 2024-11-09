using Everything.Data.Interface.Repositories;
using Everything.Data.Models.Surveys;

namespace Everything.Data.Repositories.Surveys
{
    public interface IStatusRepositoryReal : IStatusRepository<StatusData>
    {
    }

    public class StatusRepository : BaseRepository<StatusData>, IStatusRepositoryReal
    {
        public StatusRepository(WebDbContext webDbContext) : base(webDbContext)
        {
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
