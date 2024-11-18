using System.ComponentModel.DataAnnotations;
using WebPortalEverthing.Models.CustomValidationAttrubites;

namespace WebPortalEverthing.Models.MoviePoster
{
    public class MovieCreationViewModel
    {
        [Required]
        public string Name { get; set; }

        [IsUrl]
        public string Url { get; set; }

        [IsCorrectFilmDuration(70, 200, DurationOption.Minutes)]
        public int FilmDuration { get; set; }

        [NumberOfMusicalCompositions]
        public int NumberOfMusicalCompositions { get; set; }
    }
}
