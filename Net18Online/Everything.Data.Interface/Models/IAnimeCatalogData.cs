namespace Everything.Data.Interface.Models
{
    public interface IAnimeCatalogData : IBaseModel
    {
        string Name { get; set; }
        string ImageSrc { get; set; }
    }
}
