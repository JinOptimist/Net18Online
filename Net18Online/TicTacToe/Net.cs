using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TicTacToe.Field;

namespace TicTacToe
{
    public class Net
    {
        public Net() { }
        public Net(Net net)
        {
            this.Field = new List<BaseField>(net.Field);
        }

        public List<BaseField> Field { get; set; } = new List<BaseField>();

        public BaseField GetSymbol(int x, int y)
        {
            var field = Field.First(field => field.X == x && field.Y == y);
            return field;
        }

        public BaseField? this[int x, int y]
        {
            get
            {
                return Field.FirstOrDefault(cell => cell.X == x && cell.Y == y);
            }

            set
            {
                if (!Field.Any(cell => cell.X == x && cell.Y == y))
                {
                    Field.Add(value);
                    return;
                }

                var oldCell = Field.First(cell => cell.X == value.X && cell.Y == value.Y);
                Field.Remove(oldCell);
                Field.Add(value);
            }
        }
    }
}
