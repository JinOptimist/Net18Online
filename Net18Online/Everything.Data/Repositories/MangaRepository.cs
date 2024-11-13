using Everything.Data.DataLayerModels;
using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Everything.Data.Repositories
{
    public interface IMangaRepositoryReal : IMangaRepository<MangaData>
    {
        IEnumerable<MangaData> GetAllWithCharacters();
        void LinkGirl(int mangaId, int girlId);

        IEnumerable<MangaWithInfoAboutAuthor> GetMangaWithInfoAboutAuthors(int userId);
    }

    public class MangaRepository : BaseRepository<MangaData>, IMangaRepositoryReal
    {
        public MangaRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public IEnumerable<MangaData> GetAllWithCharacters()
        {
            return _dbSet
                .Include(x => x.Characters)
                .ToList();
        }

        public IEnumerable<MangaWithInfoAboutAuthor> GetMangaWithInfoAboutAuthors(int userId)
        {
            var usersMangas = _dbSet
                .Where(manga =>
                    manga.Author != null
                    && manga.Author.Id == userId);
            
            var authorsMangas = usersMangas
                    .Where(manga =>
                        manga
                            .Characters
                            .Any(character => character.Creator != null
                                && character.Creator.Id == userId))
                .Select(x => new MangaWithInfoAboutAuthor
                {
                    Name = x.Title,
                    HasCharaterWithSpecialAuthor = true
                });

            var notAuthorsMangas = usersMangas
                    .Where(manga =>
                        !manga
                            .Characters
                            .Any(character => character.Creator != null
                                && character.Creator.Id == userId))
                .Select(x => new MangaWithInfoAboutAuthor
                {
                    Name = x.Title,
                    HasCharaterWithSpecialAuthor = false
                });


            return authorsMangas
                .Union(notAuthorsMangas)
                .ToList();
        }

        public void LinkGirl(int mangaId, int girlId)
        {
            var girl = _webDbContext.Girls.First(x => x.Id == girlId);
            var manga = _dbSet.First(x => x.Id == mangaId);

            manga.Characters.Add(girl);

            _webDbContext.SaveChanges();
        }
    }
}
