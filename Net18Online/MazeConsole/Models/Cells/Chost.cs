using MazeConsole.Models.Cells;

namespace MazeConsole.Models.Cells
{

    public class Ghost : BaseCell
    {
        public Ghost(int x, int y) : base(x, y)
        {
        }

        public override char Symbol => '0';

        public override void InteractWithCell()
        {
            Console.WriteLine("BooOoo");
        }

        public override bool TryStep()
        {
            return true;
        }
    }
}

