using WebPortalEverthing.Models.AnimeCatalog;

namespace WebPortalEverthing.Models.AnimeReview
{
    public class IndexAnimeReviewViewModel
    {
        public List<AnimeReviewShortInfoViewModel> Reviews { get; set; }

        public List<AnimeCatalogNameAndIdViewModel> Animes { get; set; }
    }
}
