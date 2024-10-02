using MazeCore.Models.Cells.Character;
using MazeCore.Models.Enum;
using MazeCore.Models.Interfaces;
using MazeCore.Controllers;
using System;
using System.Linq;

namespace MazeCore.Models.Cells
{
    public class WanderingMerchant : BaseNpc, IInteractable
    {
        private Random _random = new Random();

        public WanderingMerchant(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => 'M';

        public override void Move()
        {
            var direction = GetRandomDirection();
            MoveInDirection(direction);
        }


        private Direction GetRandomDirection()
        {
            var directions = Direction.GetValues(typeof(Direction)).Cast<Direction>().ToList();
            return directions[_random.Next(directions.Count)];
        }


        private void MoveInDirection(Direction direction)
        {
            int newX = X;
            int newY = Y;

            //New coordinates based on the direction
            switch (direction)
            {
                case Direction.Up:
                    newY = Y - 1;
                    break;
                case Direction.Down:
                    newY = Y + 1;
                    break;
                case Direction.Left:
                    newX = X - 1;
                    break;
                case Direction.Right:
                    newX = X + 1;
                    break;
            }

            ///<summary>
            ///Ensure new coordinates are within bounds and navigable
            ///</summary>
            if (newX >= 0 && newX < Maze.Width && newY >= 0 && newY < Maze.Height)
            {
                var destinationCell = Maze[newX, newY];
                if (destinationCell.TryStep(this))
                {
                    X = newX;
                    Y = newY;
                }
            }
        }

        public void Interact(BaseCharacter character)
        {
            var merchantController = new WanderingMerchantController(this);
            merchantController.DisplayMenu(character);
        }

        public override bool TryStep(BaseCharacter character)
        {
            return true;
        }

        public override void InteractWithCell(BaseCharacter character)
        {
            Interact(character);
        }
    }
}
