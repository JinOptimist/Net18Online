﻿using Everything.Data.Models;
using Everything.Data.Models.Surveys;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Identity.Client;

namespace Everything.Data
{
    public class WebDbContext : DbContext
    {
        public DbSet<GirlData> Girls { get; set; }

        public DbSet<MangaData> Mangas { get; set; }

        public DbSet<GameData> Games { get; set; }

        public DbSet<GameStudiosData> Studios { get; set; }

        public DbSet<UserData> Users { get; set; }

        public DbSet<EcologyData> Ecologies { get; set; }

        public DbSet<CommentData> Comments { get; set; }

        public DbSet<MovieData> Movies { get; set; }

        public DbSet<AnimeData> Animes { get; set; }

        public DbSet<AnimeReviewData> AnimeReviews { get; set; }
        public DbSet<FilmDirectorData> FilmDirectors { get; set; }
        #region ServiceCenter
        public DbSet<TypeOfApplianceData> TypeOfAppliances { get; set; }
        public DbSet<ProducerData> Producers { get; set; }
        public DbSet<ModelData> Models { get; set; }
        public DbSet<ClientData> Clients { get; set; }
        #endregion

        public DbSet<CakeData> Cakes { get; set; }
        public DbSet<MagazinData> Magazines { get; set; }

        public DbSet<CoffeData> Coffe { get; set; }

        public DbSet<BrandData> Brands { get; set; }

        public DbSet<CoffeCompanyData> CoffeCompanies { get; set; }

        public DbSet<CoffeShopActivityData> Activities { get; set; }

        public DbSet<CartData> CartItems { get; set; }

        public DbSet<MetricData> Metrics { get; set; } // Описание таблицы с метриками
        public DbSet<LoadVolumeTestingData> LoadVolumeTestingMetrics { get; set; } // Описание таблицы с разделами LoadVolumeTesting
        public DbSet<LoadUserData> LoadUsers { get; set; }
        public DbSet<LoadChatMessageData> LoadChatMessages { get; set; }

        #region SURVEYS
        public DbSet<StatusData> Statuses { get; set; }
        public DbSet<SurveyData> Surveys { get; set; }
        public DbSet<SurveyGroupData> SurveyGroups { get; set; }
        public DbSet<QuestionData> Questions { get; set; }
        public DbSet<DocumentData> Documents { get; set; }
        public DbSet<TakingUserSurveyData> TakingUserSurveys { get; set; }
        public DbSet<AnswerToQuestionData> AnswerToQuestions { get; set; }
        #endregion

        public DbSet<DndClassData> DndClasses { get; set; }

        public DbSet<ChatMessageData> ChatMessages { get; set; }
        public DbSet<CoffeChatMessageData> CoffeChatMessages { get; set; }

        public DbSet<NotificationData> Notifications { get; set; }

        public DbSet<TagGameData> TagGame { get; set; }

        public WebDbContext() { }

        public WebDbContext(DbContextOptions<WebDbContext> contextOptions)
            : base(contextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"Net18Online\";Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MangaData>()
                .HasMany(x => x.Characters)
                .WithOne(x => x.Manga)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MangaData>()
                .HasOne(x => x.Author)
                .WithMany(x => x.CreatedMangas)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GirlData>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.CreatedGirls)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GirlData>()
                .HasMany(x => x.UsersWhoLikeIt)
                .WithMany(x => x.GirlsWhichUsersLike);

            modelBuilder.Entity<AnimeReviewData>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.CreatedAnimeReviews)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MovieData>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.CreatedMovies)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SurveyGroupData>()
                .HasMany(x => x.Surveys)
                .WithOne(x => x.SurveyGroup)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SurveyData>()
                .HasMany(x => x.Questions)
                .WithOne(x => x.Survey)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TakingUserSurveyData>()
                .HasOne(x => x.User)
                .WithMany(x => x.PassingSurveys)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TakingUserSurveyData>()
                .HasOne(x => x.Survey)
                .WithMany(x => x.PassingUsers)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AnswerToQuestionData>()
                .HasOne(x => x.TakingUserSurvey)
                .WithMany(x => x.AnswerToQuestions)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AnswerToQuestionData>()
                .HasOne(x => x.Question)
                .WithMany(x => x.Answers)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserData>()
                .HasMany(x => x.СreatorSurveyGroups)
                .WithOne(x => x.СreatorUser)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserData>()
                .HasMany(x => x.ChatMessages)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<UserData>()
                .HasMany(x => x.CoffeChatMessages)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<CoffeData>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.CreatedCoffe)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BrandData>()
                .HasMany(x => x.Coffe)
                .WithOne(x => x.Brand)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CoffeCompanyData>()
                .HasMany(x => x.Coffe)
                .WithOne(x => x.Company)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CoffeShopActivityData>()
                .HasMany(x => x.CoffeCompanies)
                .WithOne(x => x.TypeOfActivity)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CakeData>()
                .HasMany(x => x.Magazins)
                .WithMany(x => x.Cakes);


            modelBuilder.Entity<CakeData>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.CreatedCakes)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MagazinData>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.CreatedMagazins)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GameStudiosData>()
                .HasMany(x => x.Games)
                .WithOne(x => x.Studios)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LoadVolumeTestingData>()
               .HasMany(x => x.VolumeMetrics)
               .WithOne(x => x.LoadVolumeTesting)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MetricData>()
                .HasOne(x => x.LoadUserDataCreator)
                .WithMany(x => x.Metrics)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LoadVolumeTestingData>()
                .HasOne(x => x.LoadUserDataCreator)
                .WithMany(x => x.LoadVolumeTestingParts)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MetricData>()
                .HasMany(x => x.UserWhoLikeIt)
                .WithMany(x => x.MetricsWhichUsersLike);

            modelBuilder.Entity<LoadUserData>()
                .HasMany(x => x.LoadChatMessages)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<UserData>().HasKey(us => us.Id);
            modelBuilder.Entity<UserData>()
                .HasMany(p => p.Ecologies)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<EcologyData>().HasKey(ec => ec.Id);
            modelBuilder.Entity<EcologyData>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.Ecology)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(x => x.PostId);

            modelBuilder.Entity<CommentData>().HasKey(c => c.Id);
            modelBuilder.Entity<CommentData>()
                .HasOne(x => x.User)
                .WithMany(x => x.Comments)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserEcologyLikesData>().HasKey(ue => new { ue.UserId, ue.EcologyDataId });
            modelBuilder.Entity<UserEcologyLikesData>()
                .HasOne(ue => ue.User)
                .WithMany(u => u.PostsWhichUsersLike)
                .HasForeignKey(ue => ue.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserEcologyLikesData>()
                .HasOne(ue => ue.EcologyData)
                .WithMany(ec => ec.UsersWhoLikeIt)
                .HasForeignKey(ue => ue.EcologyDataId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AnimeData>()
                .HasMany(x => x.Reviews)
                .WithOne(x => x.Anime)
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

            modelBuilder.Entity<GameData>()
                .HasMany(x => x.Buyers)
                .WithMany(x => x.Games);

            modelBuilder.Entity<GameData>()
                .HasMany(x => x.UsersWhoLikedGame)
                .WithMany(x => x.GameWhichUsersLike);

            modelBuilder.Entity<GameData>()
                .HasMany(x => x.UsersWhoDislikedGame)
                .WithMany(x => x.GameWhichUsersDislike);

            modelBuilder.Entity<FilmDirectorData>()
                .HasMany(x => x.Movies)
                .WithOne(x => x.FilmDirector)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FilmDirectorData>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.CreatedFilmDirectors)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NotificationData>()
                .HasMany(x => x.UsersWhoAlreadySawIt)
                .WithMany(x => x.NotificationsWhichIAlreadySaw);
            
            modelBuilder.Entity<UserData>()
                .HasOne(x => x.TagGame)
                .WithOne(x => x.Creator)
                .HasForeignKey<TagGameData>(x => x.Id)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
