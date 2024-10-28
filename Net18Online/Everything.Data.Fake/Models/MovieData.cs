using Everything.Data.Interface.Models;

namespace Everything.Data.Fake.Models
{
    public class MovieData : BaseModel, IMovieData
    {
        public string Name { get; set; }
        public string ImageSrc { get; set; }
        public List<string> Tags { get; set; }
    }
}
