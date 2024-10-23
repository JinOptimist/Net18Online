using Everything.Data.Interface.Models;

namespace Everything.Data.Fake.Models
{
    public class MovieData : IMovieData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageSrc { get; set; }
        public List<string> Tags { get; set; }
    }
}
