using System.Text;

namespace MazeConsole.Models.Cells
{
    public class Dungeon : BaseCell
    {
        public Dungeon(int x, int y) : base(x, y)
        {

        }

        public override char Symbol => 'v';

        public override void InteractWithCell()
        {
            Console.WriteLine("Step Step Step....");
        }

        public override bool TryStep()
        {
            return true;
        }
    }
}
