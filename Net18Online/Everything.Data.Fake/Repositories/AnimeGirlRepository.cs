using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;

namespace Everything.Data.Fake.Repositories
{
    public class AnimeGirlRepository : IAnimeGirlRepository
    {
        private List<IGirlData> girls = new List<IGirlData>();

        public void Add(IGirlData data)
        {
            data.Id = girls.Any()
                ? girls.Max(x => x.Id) + 1
                : 1;

            girls.Add(data);
        }

        public void Delete(IGirlData data)
        {
            girls.Remove(data);
        }

        public List<IGirlData> GetAll()
        {
            return girls;
        }

        public IGirlData? Get(int id)
        {
            return girls.FirstOrDefault(x => x.Id == id);
        }

        public bool Any()
        {
            return girls.Any();
        }
    }
}
