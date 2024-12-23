﻿using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface ICoffeShopRepository<T> : IBaseRepository<T>
        where T : ICoffeData
    {
        public IEnumerable<T> GetDefaultCoffe();

		void UpdateCoffeName(int id, string name);
        
        void UpdateImage(int id, string url);
        
        void UpdateCost(int id, decimal cost);
    }
}