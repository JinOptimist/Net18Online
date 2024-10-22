using Everything.Data.Fake.Models;
using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;

namespace Everything.Data.Fake.Repositories
{
    public class AnimeCatalogRepository : IAnimeCatalogRepository
    {
        private List<IAnimeCatalogData> animes = new List<IAnimeCatalogData>();

        public void Add(IAnimeCatalogData data)
        {
            data.Id = animes.Any()
                ? animes.Max(x => x.Id) + 1
                : 1;

            animes.Add(data);
        }

        public void Delete(IAnimeCatalogData data)
        {
            animes.Remove(data);
        }

        public List<IAnimeCatalogData> GetAll()
        {
            return animes;
        }

        public IAnimeCatalogData? Get(int id)
        {
            return animes.FirstOrDefault(x => x.Id == id);
        }

        public bool Any()
        {
            return animes.Any();
        }
    }
}
