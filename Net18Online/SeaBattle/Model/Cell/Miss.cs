using InterfacesForSeaBattle;

namespace SeaBattle.Model.Cell
{
    public class Miss : ICell
    {
        public Miss(int x, int y, Battleground battleground)
        {
            X = x;
            Y = y;
            Battleground = battleground;
        }
        public char Symbol { get; } = '#';
        public int X { get; set; }
        public int Y { get; set; }
        public Battleground Battleground { get; set; }
    }
}
