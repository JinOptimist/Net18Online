using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class GirlData : BaseModel, IGirlData
    {
        public string Name { get; set; }
        public string ImageSrc { get; set; }
        
        // public List<string> Tags { get; set; }
    }
}
