using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Field;

namespace TicTacToe
{
    public class PlayProcess
    {
        public Net BuildNet()
        {
            var net = new Net();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var field = new EmptyField(i, j, net);
                    net.Field.Add(field);
                }
            }
            return net;
        }

        public bool IsDraw(Net net)
        {
            var emptyfield = net.Field.OfType<EmptyField>().ToList();
            if (emptyfield.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryMove(Net net, int x, int y)
        {
            if (x <= 3 && y <= 3 && x >= 1 && y >= 1)
            {
                if (net[x - 1, y - 1].Symbol == ' ')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public char CheckWin(Net net)
        {
            char s = ' ';
            for (int i = 0; i < 3; i++)
            {
                var lineCross = net.Field.OfType<Cross>().Where(field => field.Y == i).ToList();
                var lineZero = net.Field.OfType<Zero>().Where(field => field.Y == i).ToList();
                var pillarCross = net.Field.OfType<Cross>().Where(field => field.X == i).ToList();
                var pillarZero = net.Field.OfType<Zero>().Where(field => field.X == i).ToList();
                if (lineCross.Count() == 3 || pillarCross.Count() == 3)
                {
                    s = '+';
                    break;
                }
                if (lineZero.Count() == 3 || pillarZero.Count() == 3)
                {
                    s = '0';
                    break;
                }
            }
            return s;
        }

        public void Move(char s, Net net)
        {
            Console.WriteLine();
            Console.WriteLine(s + " goes. Enter coordinate:");
            var x = Console.ReadLine();
            var y = Console.ReadLine();
            while (TryMove(net, int.Parse(x), int.Parse(y)) == false)
            {
                Console.WriteLine("Enter correct coordinate:");
                x = Console.ReadLine();
                y = Console.ReadLine();
            }
            if (s == '0')
            {
                net[int.Parse(x) - 1, int.Parse(y) - 1] = new Zero(int.Parse(x) - 1, int.Parse(y) - 1, net);
            }
            else
            {
                net[int.Parse(x) - 1, int.Parse(y) - 1] = new Cross(int.Parse(x) - 1, int.Parse(y) - 1, net);
            }
            Console.Clear();
        }
    }
}
