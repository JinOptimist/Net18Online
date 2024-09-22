using System.ComponentModel;

namespace Net18Online.Models;
public enum GameMode
{
    [Description("Computer vs Player")]
    PlayerVsComputer = 1,

    [Description("Computer vs Computer")]
    ComputerVsComputer = 2,

    [Description("Player vs Player")]
    PlayerVsPlayer = 3
}
