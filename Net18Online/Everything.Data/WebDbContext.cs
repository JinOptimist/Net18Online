using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data
{
    public class WebDbContext : DbContext
    {
        public const string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"Net18Online\";Integrated Security=True;";

        public DbSet<GirlData> Girls { get; set; }

        public DbSet<UserData> Users { get; set; }

        public WebDbContext() { }

        public WebDbContext(DbContextOptions<WebDbContext> contextOptions) 
            : base(contextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CONNECTION_STRING);
            // base.OnConfiguring(optionsBuilder);
        }
    }
}
