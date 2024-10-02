using MazeCore.Models.Cells;
using MazeCore.Models.Cells.Character;

namespace MazeCore.Models
{
    public interface IMaze
    {
        IBaseCell? this[int x, int y] { get; set; }

        List<IBaseCell> Cells { get; set; }
        int Height { get; set; }
        Hero Hero { get; set; }
        List<string> HistoryOfEvents { get; set; }
        List<BaseNpc> Npcs { get; set; }
        Random Random { get; }
        int Width { get; set; }

        IBaseCell GetTopLevelItem(int x, int y);

        void ReplaceCellToGround(IBaseCell cell);
    }
}