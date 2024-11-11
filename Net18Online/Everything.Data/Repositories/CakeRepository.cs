using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Everything.Data.Repositories
{
    public interface ICakeRepositoryReal : ICakeRepository<CakeData>
    {
        bool IsUrlUniq(string url);
        int QuantityWords(string description);
    }
    public class CakeRepository : BaseRepository<CakeData>, ICakeRepositoryReal
    {
        public CakeRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public bool IsUrlUniq(string url)
        {
            return !_dbSet.Any(x => x.ImageSrc == url);
        }

        public int QuantityWords(string description)
        {
            var quantityWords = description.Split(' ');
            return quantityWords.Length;
        }

        public void UpdateDescription(int id, string newDescription)
        {
            var cakeDescription = _webDbContext.Cakes.First(x => x.Id == id);

            cakeDescription.Description = newDescription;

            _webDbContext.SaveChanges();
        }
    }
}
