namespace MazeConsole.Models.Cells
{
    public class Teleport : BaseCell
    {
        public override char Symbol => '^';

        public Teleport(int x, int y) : base(x, y)
        {
        }

        public override void InteractWithCell()
        {
            Console.WriteLine("Moving...");
        }

        public override bool TryStep()
        {
            return true;
        }
    }
}
