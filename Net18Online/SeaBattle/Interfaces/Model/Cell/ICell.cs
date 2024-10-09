using SeaBattle.Model;

namespace InterfacesForSeaBattle
{
    public interface ICell
    {
        int X { get; set; }
        int Y { get; set; }
        Battleground Battleground { get; set; }
        char Symbol { get; }
    }
}
