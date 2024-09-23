namespace MazeConsole.Models.Cells
{
    public class Wall : BaseCell
    {
        public override char Symbol => '#';

        public Wall(int x, int y) : base(x, y)
        {
        }

        public override bool TryStep()
        {
            return false;
        }

        public override void InteractWithCell()
        {
            Console.WriteLine("Boom boom");
        }
    }
}
