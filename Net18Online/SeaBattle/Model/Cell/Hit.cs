using InterfacesForSeaBattle;

namespace SeaBattle.Model.Cell
{
    public class Hit : ICell
    {
        public Hit(int x, int y, Battleground battleground)
        {
            X = x;
            Y = y;
            Battleground = battleground;
        }

        public char Symbol { get; } = 'X';
        public int X { get; set; }
        public int Y { get; set; }
        public Battleground Battleground { get; set; }
    }
}
