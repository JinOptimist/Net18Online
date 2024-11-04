﻿using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public interface IAnimeGirlRepositoryReal : IAnimeGirlRepository<GirlData>
    {
        IEnumerable<GirlData> GetWithoutManga();
    }

    public class AnimeGirlRepository : BaseRepository<GirlData>, IAnimeGirlRepositoryReal
    {
        public AnimeGirlRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public IEnumerable<GirlData> GetMostPopular()
        {
            return GetFinilizeGirl()
                .Take(3)
                .OrderByDescending(x => x.Id)
                .ToList();
        }

        public IEnumerable<GirlData> GetWithoutManga()
        {
            return _dbSet
                .Where(x => x.Manga == null)
                .ToList();
        }

        public void UpdateImage(int id, string url)
        {
            var girl = _dbSet.First(x => x.Id == id);

            girl.ImageSrc = url;

            _webDbContext.SaveChanges();
        }

        public void UpdateName(int id, string newName)
        {
            var girl = _dbSet.First(x => x.Id == id);

            girl.Name = newName;

            _webDbContext.SaveChanges();
        }

        private IQueryable<GirlData> GetFinilizeGirl()
        {
            return _dbSet
                .Where(x => !string.IsNullOrEmpty(x.ImageSrc));
        }
    }
}
