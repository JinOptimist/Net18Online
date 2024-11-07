namespace Everything.Data.Interface.Models
{
    public interface IMovieData : IBaseModel
    {
        string Name { get; set; }
        string ImageSrc { get; set; }
        //List<string> Tags { get; set; }
    }
}
