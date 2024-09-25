using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsole.Models.Cells
{
    internal class Pit : BaseCell
    {
        public Pit(int x, int y) : base(x, y)
        {
        }

        public override char Symbol => '¤';

        public override void InteractWithCell()
        {
            Console.WriteLine("Oops you fell into a pit");
        }

        public override bool TryStep()
        {
            return false;
        }
    }
}
