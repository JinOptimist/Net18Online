using WebPortalEverthing.Models.AnimeCatalog;

namespace WebPortalEverthing.Models.AnimeDescription
{
    public class IndexAnimeDescriptionViewModel
    {
        public List<AnimeDescriptionShortInfoViewModel> Descriptions { get; set; }

        public List<AnimeCatalogNameAndIdViewModel> Animes { get; set; }
    }
}
