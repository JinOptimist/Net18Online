using Everything.Data.Interface.Models;

namespace Everything.Data.Fake.Models
{
    public class CakeData : ICakeData
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string ImageSrc { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
