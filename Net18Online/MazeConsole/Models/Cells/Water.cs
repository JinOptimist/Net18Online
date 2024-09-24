namespace MazeConsole.Models.Cells
{
    public class Water : BaseCell
    {
        public override char Symbol => '~';

        public Water(int x, int y) : base(x, y)
        {
        }

        public override bool TryStep()
        {
            return true;
        }

        public override void InteractWithCell()
        {
            Console.WriteLine("Glug glug");
        }
    }
}
