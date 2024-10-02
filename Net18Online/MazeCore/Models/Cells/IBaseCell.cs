using MazeCore.Models.Cells.Character;

namespace MazeCore.Models.Cells
{
    public interface IBaseCell
    {
        IMaze Maze { get; set; }
        char Symbol { get; }
        int X { get; set; }
        int Y { get; set; }

        void AddEventInfo(string eventInfo);
        void InteractWithCell(IBaseCharacter character);
        bool TryStep(IBaseCharacter character);
    }
}