using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class CakeData : BaseModel, ICakeData
    {
        public int Rating { get; set; }
        public string ImageSrc { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
