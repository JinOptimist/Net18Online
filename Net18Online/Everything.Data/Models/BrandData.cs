using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class BrandData : BaseModel, IBrandData
    {
        public string Name { get; set; }

        public string Company { get; set; }

        public virtual List<CoffeData> Coffe { get; set; } = new List<CoffeData>();
    }
}
