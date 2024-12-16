namespace Everything.Data.Interface.Models
{
    public interface ICoffeChatMessageData : IBaseModel
    {
        public DateTime CreationTime { get; set; }
        public string Message { get; set; }
    }
}
