using GameOfLife.Models.Cells;

namespace GameOfLife.Models
{
    public class Field
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<BaseCell> Cells { get; set; } = new List<BaseCell>();
        public Random Random { get; private set; } = new Random();
    }
}