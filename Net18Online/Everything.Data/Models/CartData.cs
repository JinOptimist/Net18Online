using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class CartData : BaseModel, ICartData
    {
        public int UserId { get; set; }
        public virtual UserData User { get; set; }

        public int CoffeId { get; set; }
        public virtual CoffeData Coffe { get; set; }

        public int Quantity { get; set; }
    }
}
