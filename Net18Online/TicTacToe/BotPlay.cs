using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Field;

namespace TicTacToe
{
    public class BotPlay
    {

        public bool WinPosition(Net net)
        {
            var FreeField = net.Field.OfType<EmptyField>().ToList();
            bool win = false;
            for (int i = 0; i < FreeField.Count; i++)
            {
                var play = new PlayProcess();
                var newnet = new Net(net);

                newnet[FreeField[i].X, FreeField[i].Y] = new Zero(FreeField[i].X, FreeField[i].Y, newnet);
                if (play.CheckWin(newnet) == '0')
                {
                    net[FreeField[i].X, FreeField[i].Y] = new Zero(FreeField[i].X, FreeField[i].Y, net);
                    win = true;
                    break;
                }
            }
            return win;
        }

        public bool ProtectionPosition(Net net)
        {
            var FreeField = net.Field.OfType<EmptyField>().ToList();
            bool protect = false;
            for (int i = 0; i < FreeField.Count; i++)
            {
                var play = new PlayProcess();
                var newnet = new Net(net);

                newnet[FreeField[i].X, FreeField[i].Y] = new Cross(FreeField[i].X, FreeField[i].Y, newnet);
                if (play.CheckWin(newnet) == '+')
                {
                    net[FreeField[i].X, FreeField[i].Y] = new Zero(FreeField[i].X, FreeField[i].Y, net);
                    protect = true;
                    break;
                }
            }
            return protect;
        }

        public void BotMotion(Net net)
        {

        }

        public void PlayWithBot()
        {
            var play = new PlayProcess();
            var net = play.BuildNet();

            Random rnd = new Random();
            var randomIndex = rnd.Next(0, 9);
            net.Field[randomIndex] = new Zero(net.Field[randomIndex].X, net.Field[randomIndex].Y, net);

            Console.Clear();
            var draw = new NetDrawer();

            while (play.CheckWin(net) == ' ' && play.IsDraw(net) == false)
            {
                draw.PrintNet(net);
                play.Move('+', net);
                if (play.CheckWin(net) == '+')
                {
                    break;
                }

                if (WinPosition(net) == false)
                {
                    if (ProtectionPosition(net) == false)
                    {
                        var emptyfield = net.Field.Where(field => field.Symbol == ' ').ToList();
                        var field = rnd.Next(0, emptyfield.Count());
                        net[emptyfield[randomIndex].X, emptyfield[randomIndex].Y] = new Zero(emptyfield[randomIndex].X, emptyfield[randomIndex].Y, net);
                    }
                }
            }

            draw.PrintNet(net);
            if (play.CheckWin(net) == '+')
            {
                Console.WriteLine("You win!");
            }
            else if (play.CheckWin(net) == '0')
            {
                Console.WriteLine("I win!");
            }
            else
            {
                Console.WriteLine("Draw");
            }
        }
    }
}
