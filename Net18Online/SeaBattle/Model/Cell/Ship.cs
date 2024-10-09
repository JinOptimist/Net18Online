using InterfacesForSeaBattle;

namespace SeaBattle.Model.Cell
{
    public class Ship : ICell
    {
        public Ship(int x, int y, Battleground battleground, int countOfDeck)
        {
            X = x;
            Y = y;
            Battleground = battleground;
            _countOfDeck = countOfDeck;
        }
        private int _countOfDeck;
        public char Symbol { get; } = 'S';
        public int X { get; set; }
        public int Y { get; set; }
        public Battleground Battleground { get; set; }
    }
}
