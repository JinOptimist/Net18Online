using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data.Repositories
{
    public interface IDndClassRepositoryReal : IDndClassRepository<DndClassData>
    {
        IEnumerable<DndClassData> GetAllWithCharacters();
        void LinkSubClass(int dndClassId, int dndSubClassId);
    }

    public class DndClassRepository : BaseRepository<DndClassData>, IDndClassRepositoryReal
    {
        public DndClassRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public IEnumerable<DndClassData> GetAllWithCharacters()
        {
            return _dbSet
                .Include(x => x.SubClasses)
                .ToList();
        }

        public void LinkSubClass(int dndClassId, int dndSubClassId)
        {
            var dndSubClass = _webDbContext.DndSubClasses.First(x => x.Id == dndSubClassId);
            var dndClass = _dbSet.First(x => x.Id == dndClassId);

            dndClass.SubClasses.Add(dndSubClass);

            _webDbContext.SaveChanges();
        }
        public IEnumerable<DndClassData> GetMostPopular()
        {
            return GetFinilizeClass()
                .Take(3)
                .OrderByDescending(x => x.Id)
                .ToList();
        }
        public void UpdateImage(int id, string url)
        {
            var dndClass = _dbSet.First(x => x.Id == id);

            dndClass.ImageSrc = url;

            _webDbContext.SaveChanges();
        }

        public void UpdateName(int id, string newName)
        {
            var dndClass = _dbSet.First(x => x.Id == id);

            dndClass.Name = newName;

            _webDbContext.SaveChanges();
        }
        private IQueryable<DndClassData> GetFinilizeClass()
        {
            return _dbSet
                .Where(x => !string.IsNullOrEmpty(x.ImageSrc));
        }
    }
}