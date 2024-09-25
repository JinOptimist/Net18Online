namespace MazeConsole.Models.Cells
{
    public class Snake : BaseCell
    {
        private const int HEALTH = 20;

        public int Damage => 1;

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
