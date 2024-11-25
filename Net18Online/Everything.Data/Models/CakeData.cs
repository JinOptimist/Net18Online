using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class CakeData : BaseModel, ICakeData
    {
        public string ImageSrc { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public virtual UserData? Creator { get; set; }
        public virtual List<MagazinData>? Magazins { get; set; }
    }
}
