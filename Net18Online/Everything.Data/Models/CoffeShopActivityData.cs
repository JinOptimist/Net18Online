using Everything.Data.Interface;

namespace Everything.Data.Models
{
	public class CoffeShopActivityData : BaseModel, ICoffeShopActivityData
	{
		public string Activity { get; set; }

		public virtual List<CoffeCompanyData> CoffeCompanies { get; set; } = new();
    }
}
