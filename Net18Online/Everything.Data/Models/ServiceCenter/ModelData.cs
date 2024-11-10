using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class ModelData : BaseModel, IModelData
    {
        public string Name { get; set; }
        public int ProducerId { get; set; }
        public int TypeId { get; set; }

        public virtual TypeOfApplianceData? ModelType { get; set; }
        public virtual ProducerData? ModelProducer { get; set; }
    }
}
