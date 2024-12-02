namespace WebPortalEverthing.Models.LoadTesting.Admin
{
    public class LoadUserVewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public List<string> Roles { get; set; } = new List<string> { "Admin" };
    }
}
