using MazeConsole.Models.Cells.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MazeConsole.Models.Cells
{
    public class Chest : BaseCell
    {
        public Chest(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => 'B';

        public override void InteractWithCell(BaseCharacter character)
        {
            Console.WriteLine("Try to open");

             var Random = new Random();

            var randomNumberToDetermineAnEvent = Random.Next(1, 100);

            if (randomNumberToDetermineAnEvent <=40)
            {
                Maze.Hero.Coins++;
                Console.WriteLine($"Your have {Maze.Hero.Coins} coins");
            }
            else if (randomNumberToDetermineAnEvent > 40 && randomNumberToDetermineAnEvent <= 70)
            {
                Console.WriteLine("Here is healing potion");
                Maze.Hero.Health++;
                Console.WriteLine($"Your helth is {Maze.Hero.Health}");
            }
            else if (randomNumberToDetermineAnEvent > 70 && randomNumberToDetermineAnEvent <= 90)
            {
                Console.WriteLine("Here is nothing");
            }
            else if (randomNumberToDetermineAnEvent > 90)
            {
                Console.WriteLine("It's a trap");
                Maze.Hero.Health--;
                Console.WriteLine($"Your helth is {Maze.Hero.Health}");
            }
        }

        public override bool TryStep(BaseCharacter character)
        {
            return true;
        }
    }
}
