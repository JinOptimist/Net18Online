using MazeConsole.Models.Cells;

namespace MazeConsole.Models
{
    public class Maze
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<BaseCell> Cells { get; set; } = new List<BaseCell>();
        public static int Money { get; set; } = 0; 

        public BaseCell this[int x, int y]
        {
            get
            {
                return Cells.First(cell => cell.X == x && cell.Y == y);
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
