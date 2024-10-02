using MazeCore.Models.Cells;
using MazeCore.Models.Cells.Character;

namespace MazeCore.Controllers
{
    public class WanderingMerchantController
    {
        private readonly WanderingMerchant _merchant;

        public WanderingMerchantController(WanderingMerchant merchant)
        {
            _merchant = merchant;
        }

        public void DisplayMenu(BaseCharacter character)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("The merchant offers the following items for sale:");
                Console.WriteLine("1. Buy Healing Salve (+5 Health) - 5 Coins");
                Console.WriteLine("2. Exit");

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                        TryBuyHealingSalve(character);
                        break;
                    case ConsoleKey.D2:
                        return; 
                }
            }
        }

        private void TryBuyHealingSalve(BaseCharacter character)
        {
            if (character.Coins >= 5)
            {
                character.Coins -= 5;
                character.Health += 5;

                Console.Clear();
                Console.WriteLine("You bought a Healing Salve. +5 Health.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("You don't have enough coins.");
            }

            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey(true);
        }
    }
}
