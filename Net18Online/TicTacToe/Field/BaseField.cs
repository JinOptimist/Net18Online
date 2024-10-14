using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Field
{
    public abstract class BaseField
    {
        public BaseField(int x, int y, Net net)
        {
            X = x; Y = y;
        }

        public int X;
        public int Y;

        public abstract char Symbol { get; }
    }
}
