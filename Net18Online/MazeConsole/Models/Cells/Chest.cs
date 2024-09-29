namespace MazeConsole.Models.Cells
{
    public class Chest : BaseCell
    {
        public Chest(int x, int y) : base(x, y)
        {
        }

        public override char Symbol => 'T';

        public override bool TryStep()
        {
            return true;
        }

        public override void InteractWithCell()
        {
            Console.WriteLine("Yay! You found a chest! +10 money");
        }
    }
}