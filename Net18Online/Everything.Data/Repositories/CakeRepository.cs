using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using System;

namespace Everything.Data.Repositories
{
    public interface ICakeRepositoryReal : ICakeRepository<CakeData>
    {
    }
    public class CakeRepository : ICakeRepositoryReal
    {
        private WebDbContext _webDbContext;
        public CakeRepository(WebDbContext webDbContext)
        {
            _webDbContext = webDbContext;
        }

        public void Add(CakeData data)
        {
            _webDbContext.Cakes.Add(data);
            _webDbContext.SaveChanges();
        }

        public bool Any()
        {
            return _webDbContext.Cakes.Any();
        }

        public void Delete(CakeData data)
        {
            _webDbContext.Cakes.Remove(data);
            _webDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = _webDbContext.Cakes.FirstOrDefault(x => x.Id == id);
            _webDbContext.Cakes.Remove(data);
            _webDbContext.SaveChanges();
        }

        public CakeData? Get(int id)
        {
            return _webDbContext.Cakes.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<CakeData> GetAll()
        {
            return _webDbContext.Cakes.ToList();
        }

        public void UpdateDescription(int id, string newDescription)
        {
            var cakeDescription = _webDbContext.Cakes.First(x => x.Id == id);

            cakeDescription.Description = newDescription;

            _webDbContext.SaveChanges();
        }
    }
}
