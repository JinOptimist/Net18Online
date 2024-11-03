using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public interface IAnimeGirlRepositoryReal : IAnimeGirlRepository<GirlData>
    {
    }

    public class AnimeGirlRepository : IAnimeGirlRepositoryReal
    {
        private WebDbContext _webDbContext;

        public AnimeGirlRepository(WebDbContext webDbContext)
        {
            _webDbContext = webDbContext;
        }

        public void Add(GirlData data)
        {
            _webDbContext.Add(data);
            _webDbContext.SaveChanges();
        }

        public bool Any()
        {
            return _webDbContext.Girls.Any();
        }

        public void Delete(GirlData data)
        {
            _webDbContext.Girls.Remove(data);
            _webDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = Get(id);
            Delete(data);
        }

        public GirlData? Get(int id)
        {
            return _webDbContext.Girls.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<GirlData> GetAll()
        {
            return GetFinilizeGirl().ToList();
        }

        public IEnumerable<GirlData> GetMostPopular()
        {
            return GetFinilizeGirl()
                .Take(3)
                .OrderByDescending(x => x.Id)
                .ToList();
        }

        public void UpdateImage(int id, string url)
        {
            var girl = _webDbContext.Girls.First(x => x.Id == id);

            girl.ImageSrc = url;

            _webDbContext.SaveChanges();
        }

        public void UpdateName(int id, string newName)
        {
            var girl = _webDbContext.Girls.First(x => x.Id == id);

            girl.Name = newName;

            _webDbContext.SaveChanges();
        }

        private IQueryable<GirlData> GetFinilizeGirl()
        {
            return _webDbContext
                .Girls
                .Where(x => !string.IsNullOrEmpty(x.ImageSrc));
        }
    }
}
