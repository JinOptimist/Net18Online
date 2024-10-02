using MazeCore.Actions;
using MazeCore.Models.Cells.Character;
using MazeCore.Models.Enum;
using MazeCore.Models.Interfaces;

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

            // New coordinates based on the moving direction
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

        /// <summary>
        /// Method that passes interactions execution on UI level(to controller)
        /// </summary>
        public void Interact(BaseCharacter character)
        {
            var merchantActions = new WanderingMerchantActions(this);
            var interactionResult = merchantActions.PerformAction(character, WanderingMerchantOptions.BuyHealingSalve);

            HandleInteractionResult(interactionResult, character);
        }

        /// <summary>
        /// Interaction result is handled at the UI layer(in Controller),
        /// so this func is left empty as we using a strict UI-Business separation.
        /// </summary>
        private void HandleInteractionResult(WanderingMerchantActionResult result, BaseCharacter character)
        {
        }

        public override bool TryStep(BaseCharacter character)
        {
            return true;
        }

        /// <summary>
        /// Delegate interaction logic to the Interact method
        /// </summary>
        public override void InteractWithCell(BaseCharacter character)
        {
            Interact(character);
        }
    }
}
