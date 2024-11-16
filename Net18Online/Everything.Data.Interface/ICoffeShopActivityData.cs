using Everything.Data.Interface.Models;

namespace Everything.Data.Interface
{
    public interface ICoffeShopActivityData : IBaseModel
    {
        string Activity { get; set; }
    }
}
