using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data.Repositories
{
    public interface IDndSubClassRepositoryReal : IDndSubClassRepository<DndSubClassData>
    {
        IEnumerable<DndSubClassData> GetWithoutDndClass();
        bool HasSimilarName(string name);
        bool IsNameUniq(string name);
    }

    public class DndSubClassRepository : BaseRepository<DndSubClassData>, IDndSubClassRepositoryReal
    {
        public DndSubClassRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public IEnumerable<DndSubClassData> GetMostPopular()
        {
            return GetFinilizeSubClass()
                .Take(3)
                .OrderByDescending(x => x.Id)
                .ToList();
        }

        public IEnumerable<DndSubClassData> GetWithoutDndClass()
        {
            return _dbSet
                .Where(x => x.DndClass == null)
                .ToList();
        }

        public bool HasSimilarName(string name)
        {
            return _dbSet.Any(x => x.Name.StartsWith(name) || name.StartsWith(x.Name));
        }

        public bool IsNameUniq(string name)
        {
            return !_dbSet.Any(x => x.Name == name);
        }

        public void UpdateImage(int id, string url)
        {
            var SubClass = _dbSet.First(x => x.Id == id);

            SubClass.ImageSrc = url;

            _webDbContext.SaveChanges();
        }

        public void UpdateName(int id, string newName)
        {
            var SubClass = _dbSet.First(x => x.Id == id);

            SubClass.Name = newName;

            _webDbContext.SaveChanges();
        }

        private IQueryable<DndSubClassData> GetFinilizeSubClass()
        {
            return _dbSet
                .Where(x => !string.IsNullOrEmpty(x.ImageSrc));
        }
    }
}