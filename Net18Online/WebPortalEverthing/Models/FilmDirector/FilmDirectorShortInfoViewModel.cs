using WebPortalEverthing.Models.MoviePoster;

namespace WebPortalEverthing.Models.FilmDirector
{
    public class FilmDirectorShortInfoViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public List<MovieNameAndIdViewModel> Movies { get; set; } = new();
    }
}
