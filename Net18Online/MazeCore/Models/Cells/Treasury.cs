using MazeCore.Models;
using MazeCore.Models.Cells.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeCore.Models.Cells
{
    public class Treasury : BaseCell
    {
        public Treasury(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => 'T';

        public override void InteractWithCell(BaseCharacter character)
        {
            AddEventInfo("You found a Treasury");
                character.Coins = character.Coins + 5;
        }

        public override bool TryStep(BaseCharacter character)
        {
            return true;
        }
    }
}

