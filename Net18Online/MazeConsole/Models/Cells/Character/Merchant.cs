using MazeConsole.Models.Interfaces;

namespace MazeConsole.Models.Cells.Character
{
    internal class Merchant : BaseCharacter, IInteractable
    {
        public Merchant(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => 'M';

        /// <summary>
        /// Merchant interaction: Selling Healing Salve (+5 Health)
        /// </summary>
        public void Interact(Hero hero)
        {
            const int healingCost = 3;  
            const int healingAmount = 5; 

            if (hero.Coins >= healingCost)
            {
                hero.Coins -= healingCost;
                hero.Health += healingAmount;
                Console.WriteLine($"{hero.Name} buys a Healing Salve and restores {healingAmount} health!");
            }
            else
            {
                Console.WriteLine($"{hero.Name} doesn't have enough coins for Healing Salve.");
            }
        }

        public override bool TryStep(BaseCharacter character)
        {
            if (character is Hero hero)
            {
                Interact(hero);
            }

            return true;
        }

        public override void InteractWithCell(BaseCharacter character)
        {
            if (character is Hero hero)
            {
                Interact(hero);
            }
        }
    }
}
