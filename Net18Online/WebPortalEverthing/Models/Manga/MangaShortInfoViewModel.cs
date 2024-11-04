using WebPortalEverthing.Models.AnimeGirl;

namespace WebPortalEverthing.Models.Manga
{
    public class MangaShortInfoViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<GirlNameAndIdViewModel> Girls { get; set; } = new();
    }
}
