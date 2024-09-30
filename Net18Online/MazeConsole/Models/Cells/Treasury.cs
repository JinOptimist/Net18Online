using MazeConsole.Models.Cells;
using MazeConsole.Models.Cells.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsole.Models.Cells
{
    public class Treasury : BaseCell
    {
        public Treasury(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => 'T';

        public override void InteractWithCell(BaseCharacter character)
        {
            Console.WriteLine("You found a Treasury");
            Console.WriteLine("Press 1 to take a few coins");
            Console.WriteLine("");
            Console.WriteLine("Press 2 to search for something useful \r\n(It'll cause a lot of noise)");
            var numberStr = Console.ReadLine();
            if (int.Parse(numberStr) == 1)
            {
                character.Coins = character.Coins + 5;
            }
            else if(int.Parse(numberStr) == 2)
            {
                /// <summary>
                /// Absent due to lack of enemies and inventory.
                /// </summary>
            }
            else {
                Console.WriteLine("Due to your mistake, the treasury doors will no longer open");
            }
        }

        public override bool TryStep(BaseCharacter character)
        {
            return true;
        }
    }
}

