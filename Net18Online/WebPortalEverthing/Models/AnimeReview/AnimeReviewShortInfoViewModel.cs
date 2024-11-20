using WebPortalEverthing.Models.AnimeCatalog;

namespace WebPortalEverthing.Models.AnimeReview
{
    public class AnimeReviewShortInfoViewModel
    {
        public int Id { get; set; }

        public string Review { get; set; }

        public string UserName { get; set; }

        public AnimeCatalogNameAndIdViewModel Anime { get; set; } = new();
    }
}
