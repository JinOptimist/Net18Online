using MazeCore.Models.Cells.Character;

namespace MazeCore.Models.Interfaces
{
    public interface IInteractable
    {
        void Interact(BaseCharacter character); 
        char Symbol { get; } 
    }
}
