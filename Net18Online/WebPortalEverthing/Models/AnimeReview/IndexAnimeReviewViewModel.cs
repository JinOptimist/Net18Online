using WebPortalEverthing.Models.AnimeCatalog;

namespace WebPortalEverthing.Models.AnimeReview
{
    public class IndexAnimeReviewViewModel
    {
        public List<AnimeReviewViewModel> Index { get; set; }

        public CreateAnimeReviewViewModel NewReview { get; set; }
    }
}
