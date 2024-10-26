namespace Everything.Data.Interface.Models
{
    public interface ICoffeData : IBaseModel
    {
        string Brand { get; set; }

        string Url { get; set; }

        string Coffe { get; set; }

        float Cost { get; set; }
    }
}