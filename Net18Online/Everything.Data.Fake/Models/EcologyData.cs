using Everything.Data.Interface.Models;

namespace Everything.Data.Fake.Models
{
    public class EcologyData : BaseModel, IEcologyData
    {
        public string ImageSrc { get; set; }
        public List<string> Text { get; set; }
    }
}