using InterfacesForSeaBattle;
using InterfacesForSeaBattle;
using SeaBattle.Model.Cell;

namespace SeaBattle.Model
{
    public class Battleground
    {
        public const int WIDTH = 10;
        public const int HEIGHT = 10;
        public List<ICell> Cells { get; set; } = new();

        public ICell? this[int x, int y]
        {
            get
            {
                return Cells.FirstOrDefault(cell => cell.X == x && cell.Y == y);
            }
            set
            {
                if (!Cells.Any(cell => cell.X == x && cell.Y == y))
                {
                    Cells.Add(value);
                    return;
                }

                var oldCell = Cells.First(cell => cell.X == value.X && cell.Y == value.Y);
                Cells.Remove(oldCell);
                Cells.Add(value);
            }
        }
    }
}
