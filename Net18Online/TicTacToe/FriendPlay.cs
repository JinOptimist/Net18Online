using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class FriendPlay
    {
        public void PlayWithFriend()
        {
            var play = new PlayProcess();
            var net = play.BuildNet();
            Console.Clear();
            var draw = new NetDrawer();
            int k = 1;
            while (play.CheckWin(net) == ' ' && play.IsDraw(net) == false)
            {
                if (k % 2 == 0)
                {
                    draw.PrintNet(net);
                    play.Move('+', net);
                    k++;
                }
                else
                {
                    draw.PrintNet(net);
                    play.Move('0', net);
                    k++;
                }
            }

            if (play.CheckWin(net) == '+')
            {
                Console.Clear();
                Console.WriteLine("Cross WIN!");
            }
            else if (play.CheckWin(net) == '0')
            {
                Console.Clear();
                Console.WriteLine("Zero WIN!");
            }
            else
            {
                Console.WriteLine("Draw");
            }
        }
    }
}
