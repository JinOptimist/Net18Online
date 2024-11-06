using WebPortalEverthing.Models.AnimeCatalog;

namespace WebPortalEverthing.Models.AnimeDescription
{
    public class AnimeDescriptionShortInfoViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<AnimeCatalogNameAndIdViewModel> Animes { get; set; } = new();
    }
}
