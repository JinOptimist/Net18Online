using MazeConsole.Models.Cells;
using MazeConsole.Models.Cells.Character;

namespace MazeConsole.Models
{
    public class Maze
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<BaseCell> Cells { get; set; } = new List<BaseCell>();

        public Hero Hero { get; set; }

        public BaseCell? this[int x, int y]
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

                // Replace cell if it was
                var oldCell = Cells.First(cell => cell.X == value.X && cell.Y == value.Y);
                Cells.Remove(oldCell);
                Cells.Add(value);
            }
        }

        public BaseCell GetTopLevelItem(int x, int y)
        {
            if (Hero.X == x && Hero.Y == y)
            {
                return Hero;
            }

            return this[x, y];
        }
    }
}
