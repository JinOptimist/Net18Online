using Everything.Data.Interface;

namespace Everything.Data.Models
{
    public class CoffeCompanyData : BaseModel, ICoffeCompanyData
    {
        public string Name { get; set; }

        public virtual List<CoffeData> Coffe { get; set; } = new();

        public virtual CoffeShopActivityData? TypeOfActivity { get; set; }
    }
}
