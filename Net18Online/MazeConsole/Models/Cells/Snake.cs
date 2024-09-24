namespace MazeConsole.Models.Cells
{
    public class Snake : BaseCell
    {
        private const int _health = 20;

        public int Damage { get { return 1; } }

        public Snake(int x, int y) : base(x, y)
        {
        }

        public override char Symbol => 's';

        public override void InteractWithCell()
        {
            Console.WriteLine("Fight");
        }

        public override bool TryStep()
        {
            return true;
        }
    }
}
