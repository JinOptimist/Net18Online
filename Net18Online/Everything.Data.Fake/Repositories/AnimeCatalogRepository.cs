using Everything.Data.Fake.Models;
using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;

namespace Everything.Data.Fake.Repositories
{
    public class AnimeCatalogRepository : IAnimeCatalogRepository
    {
        private List<IAnimeCatalogData> _animes = new List<IAnimeCatalogData>();

        public void Add(IAnimeCatalogData data)
        {
            data.Id = _animes.Any()
                ? _animes.Max(x => x.Id) + 1
                : 1;

            _animes.Add(data);
        }

        public void Delete(IAnimeCatalogData data)
        {
            _animes.Remove(data);
        }

        public List<IAnimeCatalogData> GetAll()
        {
            return _animes;
        }

        public IAnimeCatalogData? Get(int id)
        {
            return _animes.FirstOrDefault(x => x.Id == id);
        }

        public bool Any()
        {
            return _animes.Any();
        }
    }
}
