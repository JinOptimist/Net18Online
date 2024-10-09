using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Field
{
    internal class EmptyField : BaseField
    {
        public EmptyField(int x, int y, Net net) : base(x, y, net)
        {
        }

        public override char Symbol => ' ';
    }
}
