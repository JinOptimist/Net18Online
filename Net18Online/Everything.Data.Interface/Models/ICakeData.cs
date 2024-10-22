namespace Everything.Data.Interface.Models
{
    public interface ICakeData
    {
        int Id { get; set; }
        int Rating { get; set; }
        string ImageSrc { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }

    }
}
