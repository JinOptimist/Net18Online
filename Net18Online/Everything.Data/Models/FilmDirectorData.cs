using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class FilmDirectorData : BaseModel, IFilmDirectorData
    {
        public string Name { get; set; }
        public string LastName { get; set; }

        public virtual List<MovieData> Films { get; set; } = new List<MovieData>();
    }
}
