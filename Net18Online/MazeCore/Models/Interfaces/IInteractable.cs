using MazeCore.Models.Cells.Character;

namespace MazeCore.Models.Interfaces
{
    public interface IInteractable
    {
        void Interact(IBaseCharacter character); 
        char Symbol { get; } 
    }
}
