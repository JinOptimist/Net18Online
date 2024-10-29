namespace WebPortalEverthing.Models.BullsAndCows
{
    public class BullsAndCowsViewModel
    {
        public int NumberOfTheFirstGamer { get; set; }
        
        public int NumberOfTheSecondGamer { get; set; }
        
        public int LengthOfNumber { get; set; } = 4;
        
        public bool IsFirstGamerWin { get; set; } = false;
        
        public bool IsSecondGamerWin { get; set; } = false;
        
        public List<string> Attempts { get; set; } = new List<string>();

        public string Turn { get; set; } = "First";
    }
}