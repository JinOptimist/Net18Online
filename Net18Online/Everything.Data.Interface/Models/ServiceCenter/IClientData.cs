namespace Everything.Data.Interface.Models
{
    public interface IClientData : IBaseModel
    {
        string Email { get; set; }
        string PhoneNumber { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        string SecondName { get; set; }
        string PassportNumber { get; set; }
        string Password { get; set; }
        int PostIndex { get; set; }
        decimal Balance { get; set; }
        string Role { get; set; }
    }
}
