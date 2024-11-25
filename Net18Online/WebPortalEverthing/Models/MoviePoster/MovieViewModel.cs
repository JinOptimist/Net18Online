namespace WebPortalEverthing.Models.MoviePoster
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string ImageSrc { get; set; }
        public List<string> Tags { get; set; }
        public string Name { get; set; }
        public string CreatorName { get; set; }
        public string FilmDirector { get; set; }
        public bool CanDelete { get; set; }
    }
}
