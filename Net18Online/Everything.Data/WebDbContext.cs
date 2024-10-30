using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data
{
    public class WebDbContext : DbContext
    {
        public const string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"Net18Online\";Integrated Security=True;";
        public const string CONNECTION_STRING_SERVICE_CENTER = "Server=localhost,1433;Database=TMS;User Id=TMS_user;Password=tms_pass;TrustServerCertificate=True;";
        public DbSet<GirlData> Girls { get; set; }

        public DbSet<UserData> Users { get; set; }

        #region ServiceCenter
        public DbSet<TypeOfApplianceData> TypeOfAppliances { get; set; }
        public DbSet<ProducerData> Producers { get; set; }
        public DbSet<ModelData> Models { get; set; }
        public DbSet<ClientData> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TypeOfApplianceData>().ToTable("TypeOfAppliances");
            modelBuilder.Entity<ProducerData>().ToTable("Producers");
            modelBuilder.Entity<ModelData>().ToTable("Models");
            modelBuilder.Entity<ClientData>().ToTable("Clients");

            modelBuilder.Entity<ModelData>()
                .HasOne<ProducerData>()
                .WithMany()
                .HasForeignKey(m => m.ProducerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ModelData>()
                .HasOne<TypeOfApplianceData>()
                .WithMany()
                .HasForeignKey(m => m.TypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        #endregion

        public WebDbContext() { }

        public WebDbContext(DbContextOptions<WebDbContext> contextOptions)
            : base(contextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CONNECTION_STRING_SERVICE_CENTER);
            // base.OnConfiguring(optionsBuilder);
        }
    }
}
