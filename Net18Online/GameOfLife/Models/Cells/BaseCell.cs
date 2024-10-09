namespace GameOfLife.Models.Cells
{
    public abstract class BaseCell
    {
        protected BaseCell(int x, int y, IField field)
        {
            X = x;
            Y = y;
            Field = field;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public abstract char Symbol { get; }
        public abstract bool DeadOrAlive();

    }
}
