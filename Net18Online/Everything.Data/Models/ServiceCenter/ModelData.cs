using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class ModelData : BaseModel, IModelData
    {
        public string Model { get; set; }
        public int ProducerId { get; set; }
        public int TypeId { get; set; }
    }
}
