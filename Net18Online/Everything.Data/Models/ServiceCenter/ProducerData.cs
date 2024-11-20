using Everything.Data.Interface.Models;
using Everything.Data.Models;

namespace Everything.Data.Models
{
    public class ProducerData : BaseModel, IProducerData
    {
        public string Producer { get; set; }

        public virtual ICollection<ModelData> ModelsOnProducer { get; set; } = new List<ModelData>();
    }
}