using Everything.Data.Models;
using Everything.Data.Models.Surveys;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Identity.Client;

namespace Everything.Data
{
    public class WebDbContext : DbContext
    {
        public const string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"Net18Online\";Integrated Security=True;";
        //public const string CONNECTION_STRING = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=12345;";
        public DbSet<GirlData> Girls { get; set; }

        public DbSet<MangaData> Mangas { get; set; }

        public DbSet<GameData> Games { get; set; }

        public DbSet<GameStudiosData> Studios { get; set; }

        public DbSet<UserData> Users { get; set; }

        public DbSet<EcologyData> Ecologies { get; set; }

        public DbSet<MovieData> Movies { get; set; }
        #region ServiceCenter
        public DbSet<TypeOfApplianceData> TypeOfAppliances { get; set; }
        public DbSet<ProducerData> Producers { get; set; }
        public DbSet<ModelData> Models { get; set; }
        public DbSet<ClientData> Clients { get; set; }
        #endregion

        public DbSet<CakeData> Cakes { get; set; }

        public DbSet<CoffeData> Coffe { get; set; }

        public DbSet<BrandData> Brands { get; set; }

        public DbSet<MetricData> Metrics { get; set; } // Описание таблицы с метриками
        public DbSet<LoadVolumeTestingData> LoadVolumeTestingMetrics { get; set; } // Описание таблицы с метриками LoadVolumeTesting

        public DbSet<StatusData> Statuses { get; set; }
        public DbSet<SurveyData> Surveys { get; set; }
        public DbSet<SurveyGroupData> SurveyGroups { get; set; }

        public WebDbContext() { }

        public WebDbContext(DbContextOptions<WebDbContext> contextOptions)
            : base(contextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CONNECTION_STRING);
            //optionsBuilder.UseNpgsql(CONNECTION_STRING);
            // base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MangaData>()
                .HasMany(x => x.Characters)
                .WithOne(x => x.Manga)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SurveyGroupData>()
                .HasMany(x => x.Surveys)
                .WithOne(x => x.SurveyGroup)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserData>()
                .HasMany(x => x.СreatorSurveyGroups)
                .WithOne(x => x.СreatorUser)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BrandData>()
                .HasMany(x => x.Coffe)
                .WithOne(x => x.Brand)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GameStudiosData>()
                .HasMany(x => x.Games)
                .WithOne(x => x.Studios)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LoadVolumeTestingData>()
               .HasMany(x => x.VolumeMetrics)
               .WithOne(x => x.LoadVolumeTesting)
               .OnDelete(DeleteBehavior.NoAction);









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
    }
}
