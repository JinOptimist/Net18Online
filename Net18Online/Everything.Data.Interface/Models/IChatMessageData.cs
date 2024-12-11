namespace Everything.Data.Interface.Models
{
    public interface IChatMessageData : IBaseModel
    {
        public DateTime CreationTime { get; set; }
        public string Message { get; set; }
    }
}
