using System.Runtime.CompilerServices;
using MazeConsole.Models.Cells.Character;

namespace MazeConsole.Models.Cells
{
    public abstract class MonsterCell : BaseCell
    {
        public bool HasMoved;
        protected MonsterCell(int x, int y, Maze maze) : base(x, y, maze)
        {
        }
         
        public abstract void Move (int x, int y, Maze maze);
        
    }
}