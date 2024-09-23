namespace MazeConsole.Models.Cells
{
    public class Ground : BaseCell
    {
        public Ground(int x, int y) : base(x, y)
        {
        }

        public override char Symbol => '.';

        public override void InteractWithCell()
        {
            Console.WriteLine("step setp");
        }

        public override bool TryStep()
        {
            return true;
        }
    }
}
