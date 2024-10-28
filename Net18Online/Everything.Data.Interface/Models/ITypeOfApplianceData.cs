namespace Everything.Data.Interface.Models
{
    public interface ITypeOfApplianceData : IBaseModel
    {
        string Name { get; set; }
        string ImageSrc { get; set; }
    }
}
