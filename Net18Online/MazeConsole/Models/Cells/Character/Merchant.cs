using MazeConsole.Models.Cells.Character;
using MazeConsole.Models.Interfaces;

namespace MazeConsole.Models.Cells
{
    internal class Merchant : BaseCharacter, IInteractable
    {
        public Merchant(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => 'M';

        public void Interact(BaseCharacter character)
        {
            // Clear the previous line
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth)); 
            Console.SetCursorPosition(0, Console.CursorTop - 1); 

            ///<summary>
            ///Interaction logic: Buying health salve +5 Health
            ///</summary>
            Console.WriteLine("The merchant offers a healing potion for 5 coins. Do you want to buy? (Y/N)");

            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Y)
            {
                if (character.Coins >= 5)
                {
                    character.Coins -= 5;
                    character.Health += 5;
                    Console.WriteLine("You bought a healing potion. +5 health.");
                }
                else
                {
                    Console.WriteLine("Not enough coins to buy the healing potion.");
                }
            }
            else
            {
                Console.WriteLine("You declined the merchant's offer.");
            }
        }

        public override bool TryStep(BaseCharacter character)
        {
            return true; //Allows the hero to move onto the merchant's cell
        }

        public override void InteractWithCell(BaseCharacter character)
        {
            Interact(character);
        }
    }
}