using Everything.Data.Interface.Repositories;

namespace WebPortalEverthing.Services
{
    public class GeneratorAnimeGirls
    {
        private IAnimeGirlRepository _animeGirlRepository;

        public GeneratorAnimeGirls(IAnimeGirlRepository animeGirlRepository)
        {
            _animeGirlRepository = animeGirlRepository;
        }

        public void Generate()
        {

        }
    }
}
