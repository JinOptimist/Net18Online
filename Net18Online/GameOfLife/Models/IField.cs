using GameOfLife.Models.Cells;

namespace GameOfLife.Models
{
    public interface IField
    {
        List<BaseCell> Cells { get; set; }
        int Width { get; set; }
        int Height { get; set; }
    }
}
