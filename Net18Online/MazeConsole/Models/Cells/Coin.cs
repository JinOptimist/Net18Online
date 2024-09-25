namespace MazeConsole.Models.Cells
{
    public class Coin : BaseCell
    {
        public Coin(int x, int y) : base(x, y)
        {
        }

        public override char Symbol => 'c';

        public override void InteractWithCell()
        {
            Console.WriteLine("Wow it's a coin");
        }

        public override bool TryStep()
        {
            return true;
        }
    }
}
