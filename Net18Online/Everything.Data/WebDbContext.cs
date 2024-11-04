using Everything.Data.Models;
using Everything.Data.Models.Surveys;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data
{
    public class WebDbContext : DbContext
    {
        public const string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"Net18Online\";Integrated Security=True;";

        public DbSet<GirlData> Girls { get; set; }
        
        public DbSet<GameData> Games { get; set; }

        public DbSet<UserData> Users { get; set; }

        public DbSet<CakeData> Cakes { get; set; }

        public DbSet<CoffeData> Coffe { get; set; }

        public DbSet<StatusData> Statuses { get; set; }
        public DbSet<SurveyData> Surveys { get; set; }
        public DbSet<SurveyGroupData> SurveyGroups { get; set; }

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
