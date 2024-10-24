namespace Everything.Data.Interface.Models
{
    public interface ICakeData : IBaseModel
    {
        int Rating { get; set; }
        string ImageSrc { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }

    }
}
