using WebPortalEverthing.Models.AnimeGirl;

namespace WebPortalEverthing.Models.Manga
{
    public class IndexMangaViewModel
    {
        public List<MangaShortInfoViewModel> Mangas { get; set; }

        public List<GirlNameAndIdViewModel> Girls { get; set; }
    }
}
