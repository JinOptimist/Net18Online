using MazeCore.Models.Cells.Character;

namespace MazeCore.Models.Cells
{
    public class Pit : BaseCell
    {
        public Pit(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => '¤';



        public override void InteractWithCell(IBaseCharacter character)
        {
            if (character is Hero hero)
            {
                if (hero.IsTrappedInPit && !hero.HasLadder)
                {
                    AddEventInfo("You are trapped in the pit. Press L to buy a ladder.");
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.L)
                    {
                        hero.HasLadder = true;
                        hero.IsTrappedInPit = false;
                        AddEventInfo("You can now escape the pit.");
                    }
                }
            };

        }



        public override bool TryStep(IBaseCharacter character)
        {
            if (character is Hero hero)
            {
                if (!hero.HasLadder)
                {

                    hero.IsTrappedInPit = true;
                    return true;
                }
                else
                {
                    return true;
                }
            }

            return true;
        }
    }

}
