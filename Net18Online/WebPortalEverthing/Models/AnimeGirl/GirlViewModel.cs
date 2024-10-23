namespace WebPortalEverthing.Models.AnimeGirl
{
    public class GirlViewModel
    {
        public int Id { get; set; }
        public string ImageSrc { get; set; }

        public List<string> Tags { get; set; }
        public string Name { get; internal set; }
    }
}
