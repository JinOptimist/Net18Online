using MazeConsole.Models.Cells.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsole.Models.Cells
{
    internal class Pit : BaseCell
    {
        public Pit(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => '¤';



        public override void InteractWithCell(BaseCharacter character)
        {
            Console.WriteLine("Oops you fell into a pit");
        }



        public override bool TryStep(BaseCharacter character)
        {
            return true; 
        }
    }
}
