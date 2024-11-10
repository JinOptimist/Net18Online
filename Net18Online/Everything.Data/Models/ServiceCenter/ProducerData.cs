using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class ProducerData : BaseModel, IProducerData
    {
        public string Producer { get; set; }

        public virtual List<ModelData> ModelsOnProducer { get; set; } = new List<ModelData>();
    }
}
