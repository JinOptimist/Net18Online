using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    public record HistoryItem(GameState PreviousStateType, PlayerMark Mark, Cell Cell);
}

