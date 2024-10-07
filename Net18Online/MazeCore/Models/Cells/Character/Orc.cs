using MazeCore.Helpers;

namespace MazeCore.Models.Cells.Character
{
    public class Orc : BaseNpc
    {
        private Random _randomAtack = new Random();
        public Orc(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => 'R';

        public override void InteractWithCell(IBaseCharacter character)
        {
            var ChanceForDifferentAttacksAtack = _randomAtack.Next(1, 3);
            if (ChanceForDifferentAttacksAtack == 1){
                character.Health--;
                AddEventInfo($"Orc fight back to {character.Health}");
            }
            else if (ChanceForDifferentAttacksAtack == 2)
            {
                AddEventInfo($"Orc misses you");
            }
            else if (ChanceForDifferentAttacksAtack == 3)
            {
                character.Health--;
                character.Health--;
                AddEventInfo($"Orc attacks twice to {character.Health}");
            }
        }

        public override void Move()
        {
            var nearGrounds = MazeHelper.GetNearCells<BaseCell>(Maze, this);
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
