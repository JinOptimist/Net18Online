namespace Everything.Data.Interface.Models
{
    public interface IModelData : IBaseModel
    {
        string Model { get; set; }
        int ProducerId { get; set; }
        int TypeId { get; set; }
    }
}
