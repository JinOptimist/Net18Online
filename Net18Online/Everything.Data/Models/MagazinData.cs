using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class MagazinData : BaseModel, IMagazinData
    {
        public string Name { get; set; }

        public virtual UserData? Creator { get; set; }
        public virtual List<CakeData>? Cakes { get; set; }
    }
}
