namespace Everything.Data.Interface.Models
{
    public interface IMovieData
    {
        int Id { get; set; }
        string Name { get; set; }
        string ImageSrc { get; set; }
        List<string> Tags { get; set; }
    }
}
