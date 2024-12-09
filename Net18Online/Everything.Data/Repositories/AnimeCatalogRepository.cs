﻿using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public interface IAnimeCatalogRepositoryReal : IAnimeCatalogRepository<AnimeData>
    {
    }

    public class AnimeCatalogRepository : BaseRepository<AnimeData>, IAnimeCatalogRepositoryReal
    {
        public AnimeCatalogRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }
    }
}
