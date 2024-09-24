using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MazeConsole.Models.Cells
{
    public class Chest : BaseCell
    {
        public Chest(int x, int y) : base(x, y)
        {
        }

        public override char Symbol => 'C';

        public override void InteractWithCell()
        {
            Console.WriteLine("Try to open");
        }

        public override bool TryStep()
        {
            return false;
        }
    }
}
