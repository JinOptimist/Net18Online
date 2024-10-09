using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Field
{
    public class Zero : BaseField
    {
        public Zero(int x, int y, Net net) : base(x, y, net)
        {
        }

        public override char Symbol => '0';
    }
}
