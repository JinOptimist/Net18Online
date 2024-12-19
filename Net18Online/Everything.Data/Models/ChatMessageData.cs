using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class ChatMessageData : BaseModel, IChatMessageData
    {
        public DateTime CreationTime { get; set; }
        public string Message { get; set; } 
        public virtual UserData? User { get; set; }
    }
}
