using WebPortalEverthing.Models.AnimeCatalog;

namespace WebPortalEverthing.Models.AnimeReview
{
    public class AnimeReviewViewModel
    {
        public List<AnimeReviewShortInfoViewModel> Reviews { get; set; }

        public AnimeCatalogNameAndIdViewModel Anime { get; set; }
    }
}
