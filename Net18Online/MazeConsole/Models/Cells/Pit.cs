using MazeConsole.Models.Cells.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsole.Models.Cells
{
    internal class Pit : BaseCell
    {
        public Pit(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override char Symbol => '¤';



        public override void InteractWithCell(BaseCharacter character)
        {
            if (character is Hero hero) 
            {
                if (hero.IsTrappedInPit && !hero.HasLadder)
                {
                    Console.WriteLine("You are trapped in the pit. Press L to buy a ladder.");
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.L)
                    {
                        hero.HasLadder = true;  
                        hero.IsTrappedInPit = false;  
                        Console.WriteLine("You can now escape the pit.");
                    }
                }
            };
           
        }



        public override bool TryStep(BaseCharacter character)
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
