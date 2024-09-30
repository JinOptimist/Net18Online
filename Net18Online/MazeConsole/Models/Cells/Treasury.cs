using MazeConsole.Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsole.Models.Cells
{
    public class Treasury : BaseCell
    {
        public Treasury(int x, int y) : base(x, y)
        {
        }

        public override char Symbol => 'T';

        public override void InteractWithCell()
        {
            Console.WriteLine("step setp");
        }

        public override bool TryStep()
        {
            return true;
        }
    }
}

