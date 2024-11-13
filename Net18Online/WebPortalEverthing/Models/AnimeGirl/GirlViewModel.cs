namespace WebPortalEverthing.Models.AnimeGirl
{
    public class GirlViewModel
    {
        public int Id { get; set; }
        public string ImageSrc { get; set; }

        public List<string> Tags { get; set; }
        public string Name { get; set; }

        public string CreatorName { get; set; }
        public string MangaName { get; set; }

        public bool CanDelete { get; set; }
    }
}
