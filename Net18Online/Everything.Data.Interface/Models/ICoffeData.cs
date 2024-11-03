namespace Everything.Data.Interface.Models
{
    public interface ICoffeData : IBaseModel
    {
        string Brand { get; set; }

        string Url { get; set; }

        string Coffe { get; set; }

        decimal Cost { get; set; }
    }
}