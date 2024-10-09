using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Field;

namespace TicTacToe
{
    public class PlayControler
    {

        public void StartGame()
        {

            Console.WriteLine("Do you want with:1) friend or 2)bot? (1 / 2)");
            var choice = Console.ReadLine();
            if (choice == "1")
            {
                var play = new FriendPlay();
                play.PlayWithFriend();
            }
            else
            {
                var play = new BotPlay();
                play.PlayWithBot();
            }
        }
    }
}
