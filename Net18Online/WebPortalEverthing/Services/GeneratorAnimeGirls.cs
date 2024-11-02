using Everything.Data.Interface.Repositories;
using Everything.Data.Repositories;

namespace WebPortalEverthing.Services
{
    public class GeneratorAnimeGirls
    {
        private IAnimeGirlRepositoryReal _animeGirlRepository;

        public GeneratorAnimeGirls(IAnimeGirlRepositoryReal animeGirlRepository)
        {
            _animeGirlRepository = animeGirlRepository;
        }

        public void Generate()
        {

        }
    }
}
