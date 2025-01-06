using LoadTestingMinimalApi.DBstuff.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LoadTestingMinimalApi.DBstuff
{
    public class ChatDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ChatDbContext(DbContextOptions<ChatDbContext> contextOptions, IConfiguration configuration)
            : base(contextOptions)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("ChatDb");
            optionsBuilder.UseSqlServer(connectionString);
            // optionsBuilder.UseNpgsql(connectionString);
        }

       public DbSet<MetricData> Metrics { get; set; } 

        /* Ниже без файла appsettings.json:
        public const string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"LoadTestingMinimalApi\";Integrated Security=True;";
        public const string CONNECTION_STRING = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=12345;";

        public ChatDbContext(DbContextOptions<ChatDbContext> contextOptions)
                : base(contextOptions) { }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(CONNECTION_STRING);
                //optionsBuilder.UseNpgsql(CONNECTION_STRING);
                // base.OnConfiguring(optionsBuilder);
            }*/
    }

}
