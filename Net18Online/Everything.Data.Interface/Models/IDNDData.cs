namespace Everything.Data.Interface.Models
{
    public interface IDNDData : IBaseModel
    {
        string Name { get; set; }
        string ImageSrc { get; set; }
        //List<string> Tags { get; set; }
    }
}
