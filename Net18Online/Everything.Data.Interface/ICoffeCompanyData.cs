using Everything.Data.Interface.Models;

namespace Everything.Data.Interface
{
    public interface ICoffeCompanyData : IBaseModel
    {
        string Name { get; set; }
    }
}
