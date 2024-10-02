using MazeCore.Helpers;

namespace MazeCore.Models.Cells.Character
{
    public class Alcoholic : BaseNpc
    {
        public Alcoholic(int x, int y, Maze maze) : base(x, y, maze)
        {

        }

        public override char Symbol => 'ъ';

        public override bool TryStep(BaseCharacter character)
        {
            return true;
        }

        public override void InteractWithCell(BaseCharacter character)
        {
            var heroImmunity = Maze.Random.Next(1, 3);
            var sideEffect = "";

            switch (heroImmunity)
            {
                case 1:
                    character.Health--;
                    sideEffect = $"You feel something wrong";
                    break;
                case 2:
                    character.Health++;
                    sideEffect = $"Oooh, so good";
                    break;
            }

            AddEventInfo($"Alcoholic treats. {sideEffect}. He went on a journey to get a new bottle");
            Maze.Npcs.Remove(this);
        }

        public override void Move()
        {
            var nearCell = MazeHelper.GetNearCells<BaseCell>(Maze, this);

            if (!nearCell.Any())
            {
                return;
            }

            var destinationCell = MazeHelper.GetRandom(Maze, nearCell);

            if (destinationCell.TryStep(this))
            {
                X = destinationCell.X;
                Y = destinationCell.Y;
            }
        }
    }
}
