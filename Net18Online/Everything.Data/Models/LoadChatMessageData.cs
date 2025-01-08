using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class LoadChatMessageData : BaseModel, IChatMessageData
    {
        public DateTime CreationTime { get; set; }
        public string Message { get; set; } 
        public virtual LoadUserData? User { get; set; }

        public int? ToUserId { get; set; } // Поле для ID целевого пользователя
    }
}
