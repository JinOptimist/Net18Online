using MazeCore.Models.Cells;
using MazeCore.Models.Cells.Character;

namespace MazeCore.Models
{
    public class Maze : IMaze
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<IBaseCell> Cells { get; set; } = new List<IBaseCell>();

        /// <summary>
        /// NPC enemies and alias
        /// </summary>
        public List<BaseNpc> Npcs { get; set; } = new List<BaseNpc>();

        public Random Random { get; private set; } = new Random();

        public Hero Hero { get; set; }

        public List<string> HistoryOfEvents { get; set; } = new();

        public IBaseCell? this[int x, int y]
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

        public IBaseCell GetTopLevelItem(int x, int y)
        {
            if (Hero.X == x && Hero.Y == y)
            {
                return Hero;
            }

            var enemyFromMaze = Npcs.FirstOrDefault(enemy => enemy.X == x && enemy.Y == y);
            if (enemyFromMaze is not null)
            {
                return enemyFromMaze;
            }

            return this[x, y];
        }
    
        public void ReplaceCellToGround(IBaseCell cell)
        {
            var ground = new Ground(cell.X, cell.Y, this);
            Cells.Remove(cell);
            Cells.Add(ground);
        }
    }
}
