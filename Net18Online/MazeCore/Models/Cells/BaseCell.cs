using MazeCore.Models;
using MazeCore.Models.Cells.Character;

namespace MazeCore.Models.Cells
{
    public abstract class BaseCell
    {
        protected BaseCell(int x, int y, Maze maze)
        {
            X = x;
            Y = y;
            Maze = maze;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Maze Maze { get; set; }

        public abstract char Symbol { get; }

        public abstract bool TryStep(BaseCharacter character);

        public abstract void InteractWithCell(BaseCharacter character);

        public void AddEventInfo(string eventInfo)
        {
            Maze.HistoryOfEvents.Add(eventInfo);
        }
    }
}
