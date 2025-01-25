using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class TagGameData : BaseModel, ITagGameData
    {
        public int[]? Tags { get; set; }
        public virtual UserData? Creator { get; set; }
    }
}
