using WebPortalEverthing.Models.FilmDirector;

namespace WebPortalEverthing.Models.MoviePoster.Profile
{
    public class ProfileViewModel
    {
        public string UserName { get; set; }
        public List<FilmDirectorShortInfoViewModel> FilmDirectors { get; set; }

        public List<MovieShortInfoViewModel> Movies { get; set; }
    }
}
