namespace GameOfLife.Models.Cells
{
    public class liveCell : BaseCell
    {
        public liveCell(int x, int y, IField field) : base(x, y, field)
        {
        }
        public override char Symbol => 'X';
        public override bool DeadOrAlive()
        {
            return true;
        }
    }
}