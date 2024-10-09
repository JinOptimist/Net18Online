using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class NetDrawer
    {
        public void PrintNet(Net net)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var s = net.Field.First(field => field.X == i && field.Y == j);
                    Console.Write("|" + s.Symbol);
                }
                Console.Write("|");
                Console.WriteLine();
            }
        }
    }
}
