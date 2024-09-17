using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net18Online.Interfaces
{
    internal interface IGuess
    {
        /// <summary>
        /// Number which players try to guess
        /// </summary>
        int Number { get; set; }
        /// <summary>
        /// Is this player bot or real player
        /// </summary>
        bool isBot { get; set; }
        /// <summary>
        /// Current attempt count of player
        /// </summary>
        int AttemptCount { get; set; }
        string Name { get; set; }
    }
}
