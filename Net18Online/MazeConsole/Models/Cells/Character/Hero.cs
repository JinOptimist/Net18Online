using MazeConsole.Models.Interfaces;

namespace MazeConsole.Models.Cells.Character
{
    public class Hero : BaseCharacter
    {
        public Hero(int x, int y, Maze maze) : base(x, y, maze)
        {
            Health = 100;
            Coins = 10; 
        }

        public string Name { get; set; } = "Hero";

        public override char Symbol => '@';

        public override void InteractWithCell(BaseCharacter character)
        {
            if (character is IInteractable interactable)
            {
                interactable.Interact(this);
            }
        }
    }
}
