﻿using MazeCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeCore.Models.Cells.Character
{
    public class Wolf : BaseNpc
    {
        private Random _random = new Random();

        public Wolf(int x, int y, Maze maze) : base(x, y, maze)
        {
            _random = new Random();
        }

        public Wolf(int x, int y, Maze maze, Random random) : base(x, y, maze)
        {
            _random = random ?? new Random();
        }

        public override char Symbol => 'W';

        public override void InteractWithCell(IBaseCharacter character)
        {
            var crit = _random.Next(1,6);
            if (crit == 3)
            {
                var damage = 2;                
                character.Health -= damage;
                AddEventInfo($"Wolf attack with critical damage on {character.Health}");
            }
            else
            {
                character.Health--;
                AddEventInfo($"Wolf attack on {character.Health}");
            }
            
        }

        public override void Move()
        {
            var nearGrounds = MazeHelper.GetNearCells<BaseCell>(Maze, this)
                .Where(cell => !(cell is Wall))
                .ToList();
            if (!nearGrounds.Any())
            {
                return;
            }

            var destinationCell = MazeHelper.GetRandom(Maze, nearGrounds);
            if (destinationCell.TryStep(this))
            {
                X = destinationCell.X;
                Y = destinationCell.Y;
            }
        }
    }
}
