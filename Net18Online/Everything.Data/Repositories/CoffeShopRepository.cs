﻿using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data.Repositories
{
    public interface IKeyCoffeShopRepository : ICoffeShopRepository<CoffeData>
    {
        void Create(CoffeData dataCoffe, int currentUserId, int brandId);
        IEnumerable<CoffeData> GetAllWithCreatorsAndBrand();
        IEnumerable<CoffeData> GetAllByCreatorId(int creatorId);
        IEnumerable<CoffeData> GetCoffeByBrandName(string brandName);
    }

    public class CoffeShopRepository : BaseRepository<CoffeData>, IKeyCoffeShopRepository
    {
        public CoffeShopRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public void Create(CoffeData dataCoffe, int currentUserId, int brandId)
        {
            var creator = _webDbContext.Users.First(x => x.Id == currentUserId);
            var brand = _webDbContext.Brands.First(x => x.Id == brandId);

            dataCoffe.Creator = creator;
            dataCoffe.Brand = brand;

            Add(dataCoffe);
        }

        public IEnumerable<CoffeData> GetCoffeByBrandName(string brandName)
        {
            return _dbSet
                .Include(x => x.Creator)
                .Include(x => x.Brand)
                .Where(x => x.Brand != null && x.Brand.Name == brandName)
                .ToList();
        }

        public IEnumerable<CoffeData> GetAllByCreatorId(int creatorId)
        {
            return _dbSet
                .Where(x => x.Creator != null 
                         && x.Creator.Id == creatorId)
                .ToList();
        }

        public IEnumerable<CoffeData> GetAllWithCreatorsAndBrand()
        {
            return _dbSet
                .Include(x => x.Creator)
                .Include(x => x.Brand)
                .ToList();
        }

        public IEnumerable<CoffeData> GetDefaultCoffe()
        {
            return SerializeObject()
                .Where(x => x.Coffe == "Latte" || x.Coffe == "Raf" || x.Coffe == "Americano")
                .ToList();
        }

        public void UpdateCoffeName(int id, string name)
        {

            var item = _dbSet.First(x => x.Id == id);

            item.Coffe = name;

            _webDbContext.SaveChanges();
        }

        public void UpdateCost(int id, decimal cost)
        {

            var item = _dbSet.First(x => x.Id == id);

            item.Cost = cost;

            _webDbContext.SaveChanges();
        }

        public void UpdateImage(int id, string url)
        {

            var item = _dbSet.First(x => x.Id == id);

            item.Url = url;

            _webDbContext.SaveChanges();
        }

        private IQueryable<CoffeData> SerializeObject()
        {
            return _dbSet
                .Where(x => !string.IsNullOrEmpty(x.Url));
        }
    }
}