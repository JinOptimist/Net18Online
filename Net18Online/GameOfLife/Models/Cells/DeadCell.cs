namespace GameOfLife.Models.Cells
{
    public class DeadCell : BaseCell
    {
        public DeadCell(int x, int y, IField field) : base(x, y, field)
        {
        }

        public override char Symbol => 'O';
        public override bool DeadOrAlive()
        {
            return false;
        }
    }
}
