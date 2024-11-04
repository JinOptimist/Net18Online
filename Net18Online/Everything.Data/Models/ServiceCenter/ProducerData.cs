using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class ProducerData : BaseModel, IProducerData
    {
        public string Producer { get; set; }
    }
}
