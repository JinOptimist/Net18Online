using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;

namespace Everything.Data.Fake.Repositories
{
    public class CakeRepository : ICakeRepository
    {
        private List<ICakeData> _cakes = new List<ICakeData>();

        public void Add(ICakeData data)
        {
            data.Id = _cakes.Any()
                ? _cakes.Max(x => x.Id) + 1
                : 1;

            _cakes.Add(data);
        }

        public bool Any()
        {
            return _cakes.Any();
        }

        public void Delete(ICakeData data)
        {
            _cakes.Remove(data);
        }

        public ICakeData? Get(int id)
        {
            return _cakes.FirstOrDefault(cake => cake.Id == id);
        }

        public List<ICakeData> GetAll()
        {
            return _cakes;
        }
    }
}
