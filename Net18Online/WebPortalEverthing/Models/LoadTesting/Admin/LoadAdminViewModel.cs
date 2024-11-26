namespace WebPortalEverthing.Models.LoadTesting.Admin
{
    public class LoadAdminViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Roles { get; set; } = new List<string> { "Admin" };
    }
}
