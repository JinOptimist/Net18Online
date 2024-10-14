namespace MazeCore.Models.Cells.Character
{
    public interface IBaseCharacter : IBaseCell
    {
        int Coins { get; set; }
        int Health { get; set; }
        int Magic { get; set; }

        bool TryStep(IBaseCharacter character);
    }
}