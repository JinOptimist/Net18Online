﻿using MazeCore.Models.Cells.Character;

namespace MazeCore.Models.Cells
{
    public class Chest : BaseCell
    {
        public Chest(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        private bool IsChestOpen = false;

        public override char Symbol => 'B';

        public override void InteractWithCell(IBaseCharacter character)
        {
            AddEventInfo("Try to open");

            var Random = new Random();

            var randomNumberToDetermineAnEvent = Random.Next(1, 100);

            bool IsChestOpen = false;

            if (IsChestOpen == false)
            {
                if (randomNumberToDetermineAnEvent <= 40)
                {
                    Maze.Hero.Coins++;
                    AddEventInfo($"Your have {Maze.Hero.Coins} coins");
                    IsChestOpen = true;
                }
                else if (randomNumberToDetermineAnEvent > 40 && randomNumberToDetermineAnEvent <= 70)
                {
                    AddEventInfo("Here is healing potion");
                    Maze.Hero.Health++;
                    AddEventInfo($"Your helth is {Maze.Hero.Health}");
                    IsChestOpen = true;
                }
                else if (randomNumberToDetermineAnEvent > 70 && randomNumberToDetermineAnEvent <= 90)
                {
                    AddEventInfo("Here is nothing");
                    IsChestOpen = true;
                }
                else if (randomNumberToDetermineAnEvent > 90)
                {
                    AddEventInfo("It's a trap");
                    Maze.Hero.Health--;
                    AddEventInfo($"Your helth is {Maze.Hero.Health}");
                    IsChestOpen = true;
                }
            }
            else
            {
                AddEventInfo("The chest are empty already");
            }
        }

        public override bool TryStep(IBaseCharacter character)
        {
            return true;
        }
    }
}
