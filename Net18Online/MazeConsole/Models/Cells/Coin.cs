namespace MazeConsole.Models.Cells
{
    public class Coin : BaseCell
    {
        public Coin(int x, int y) : base(x, y)
        {
        }

        public override char Symbol => 'C';

        public override bool TryStep()
        {
            return true;
        }

        public override void InteractWithCell()
        {
            Console.WriteLine("Yahoo! You found a coin! +1 money");
        }
    }
}