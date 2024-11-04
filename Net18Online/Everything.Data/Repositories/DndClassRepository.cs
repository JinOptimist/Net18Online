using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public interface IDndClassRepositoryReal : IDndClassRepository<DndClassData>
    {
    }

    public class DndClassRepository : IDndClassRepositoryReal
    {
        private WebDbContext _webDbContext;

        public DndClassRepository(WebDbContext webDbContext)
        {
            _webDbContext = webDbContext;
        }

        public void Add(DndClassData data)
        {
            _webDbContext.Add(data);
            _webDbContext.SaveChanges();
        }

        public bool Any()
        {
            return _webDbContext.DndClasses.Any();
        }

        public void Delete(DndClassData data)
        {
            _webDbContext.DndClasses.Remove(data);
            _webDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = Get(id);
            Delete(data);
        }

        public DndClassData? Get(int id)
        {
            return _webDbContext.DndClasses.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<DndClassData> GetAll()
        {
            return GetFinilizeGirl().ToList();
        }

        public IEnumerable<DndClassData> GetMostPopular()
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
            var dndClass = _webDbContext.DndClasses.First(x => x.Id == id);

            dndClass.Name = newName;

            _webDbContext.SaveChanges();
        }

        private IQueryable<DndClassData> GetFinilizeGirl()
        {
            return _webDbContext
                .DndClasses
                .Where(x => !string.IsNullOrEmpty(x.ImageSrc));
        }
    }
}