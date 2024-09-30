﻿using MazeConsole.Models.Cells;
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
            Console.WriteLine("step setp");
        }

        public override bool TryStep(BaseCharacter character)
        {
            return true;
        }
    }
}

