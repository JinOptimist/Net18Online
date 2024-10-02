using MazeCore.Models.Cells.Character;

namespace MazeCore.Models.Cells
{
    public abstract class BaseCell : IBaseCell
    {
        protected BaseCell(int x, int y, IMaze maze)
        {
            X = x;
            Y = y;
            Maze = maze;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public IMaze Maze { get; set; }

        public abstract char Symbol { get; }

        public abstract bool TryStep(IBaseCharacter character);

        public abstract void InteractWithCell(IBaseCharacter character);

        public void AddEventInfo(string eventInfo)
        {
            Maze.HistoryOfEvents.Add(eventInfo);
        }
    }
}
