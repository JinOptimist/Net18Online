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
            if (character is not Hero hero)
            {
                return;
            }
            if (hero.IsTrappedInPit && !hero.HasLadder)
            {
                
                character.Coins--;
                hero.HasLadder = true;
                hero.IsTrappedInPit = false;
                AddEventInfo($"You bought a ladder and got out. Your Coin {character.Coins}");

            }
        }



        public override bool TryStep(IBaseCharacter character)
        {
            AddEventInfo("Let's go");
            if (character is not Hero hero)
            {
                return true;
            }
            if (!hero.HasLadder)
            {
                hero.IsTrappedInPit = true;
                AddEventInfo("Boom you're in a pit");
            }
            
            return true;
        }
    }

}
