using SeaBattle.Model;
using SeaBattle.Model.Cell;

namespace SeaBattle
{
    public class BattlegroundDrawer
    {
        public void DrawYourGround(Battleground battleground)
        {
            Console.Clear();
            DrowHorizontNumber();

            for (var y = 0; y < Battleground.HEIGHT; y++)
            {
                Console.Write($"{y} ");
                for (var x = 0; x < Battleground.WIDTH; x++)
                {
                    var cell = battleground[x, y];
                    Console.Write($"{cell.Symbol} ");
                }

                Console.WriteLine();
            }
        }

        public void DrawBatlleground(Battleground battlegroundFirstPlayer, Battleground battlegroundSecondPlayer)
        {
            Console.Clear();

            Console.WriteLine($"First player:{new String(' ', 9)}Second player:");
            DrowHorizontNumber(2);

            for (var y = 0; y < Battleground.HEIGHT; y++)
            {
                DrowHorizontBattleground(battlegroundFirstPlayer, y);

                DrowHorizontBattleground(battlegroundSecondPlayer, y);

                Console.WriteLine();
            }
        }
        public void DrowHorizontNumber(int countLand = 1)
        {
            for (var i = 0; i < countLand; i++)
            {
                Console.Write(" ");
                for (var x = 0; x < Battleground.WIDTH; x++)
                {
                    Console.Write($" {x}");
                }
                Console.Write(" ");
            }
            Console.WriteLine();
        }
        public void DrowHorizontBattleground(Battleground battleground, int y)
        {
            Console.Write($"{y} ");
            for (var x = 0; x < Battleground.WIDTH; x++)
            {
                if (battleground[x, y] is Ship)
                {
                    Console.Write("~ ");
                    continue;
                }
                Console.Write($"{battleground[x, y].Symbol} ");
            }
        }
    }
}
