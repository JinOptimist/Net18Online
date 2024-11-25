namespace Everything.Data.Interface.Models
{
    public interface ICakeData : IBaseModel
    {
        string ImageSrc { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }
    }
}
