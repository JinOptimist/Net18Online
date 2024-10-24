using Everything.Data.Interface.Models;

namespace Everything.Data.Fake.Models
{
    public class CoffeData : BaseModel, ICoffeData
    {
        public string Brand { get; set; }

        public string Url { get; set; }

        public string Coffe { get; set; }

        public float Cost { get; set; }
    }
}