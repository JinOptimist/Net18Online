namespace Everything.Data.Interface.Models
{
    public interface IUser : IBaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public decimal Coins { get; set; }
        public string AvatarUrl { get; set; }
    }
}
