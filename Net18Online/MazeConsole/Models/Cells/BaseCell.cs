namespace MazeConsole.Models.Cells
{
    public abstract class BaseCell
    {
        public BaseCell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public abstract char Symbol { get; }

        public abstract bool TryStep();

        public abstract void InteractWithCell();
    }
}
