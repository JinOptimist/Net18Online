namespace Everything.Data.Interface.Models
{
    public interface ICoffeData
    {
        int Id { get; set; }

        string Brand { get; set; }

        string Url { get; set; }

        string Coffe { get; set; }

        float Cost { get; set; }
    }
}