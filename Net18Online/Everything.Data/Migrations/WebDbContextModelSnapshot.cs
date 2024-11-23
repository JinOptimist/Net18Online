﻿// <auto-generated />
using System;
using Everything.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Everything.Data.Migrations
{
    [DbContext(typeof(WebDbContext))]
    partial class WebDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CakeDataMagazinData", b =>
                {
                    b.Property<int>("CakesId")
                        .HasColumnType("int");

                    b.Property<int>("MagazinsId")
                        .HasColumnType("int");

                    b.HasKey("CakesId", "MagazinsId");

                    b.HasIndex("MagazinsId");

                    b.ToTable("CakeDataMagazinData");
                });

            modelBuilder.Entity("Everything.Data.Models.AnimeData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageSrc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Animes");
                });

            modelBuilder.Entity("Everything.Data.Models.AnimeReviewData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AnimeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Review")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnimeId");

                    b.ToTable("AnimeReviews");
                });

            modelBuilder.Entity("Everything.Data.Models.BrandData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Everything.Data.Models.CakeData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageSrc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Cakes");
                });

            modelBuilder.Entity("Everything.Data.Models.ClientData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassportNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostIndex")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients", (string)null);
                });

            modelBuilder.Entity("Everything.Data.Models.CoffeCompanyData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TypeOfActivityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TypeOfActivityId");

                    b.ToTable("CoffeCompanies");
                });

            modelBuilder.Entity("Everything.Data.Models.CoffeData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("Coffe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Coffe");
                });

            modelBuilder.Entity("Everything.Data.Models.CoffeShopActivityData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Activity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("Everything.Data.Models.CommentData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Everything.Data.Models.EcologyData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageSrc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Ecologies");
                });

            modelBuilder.Entity("Everything.Data.Models.FilmDirectorData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FilmDirectors");
                });

            modelBuilder.Entity("Everything.Data.Models.GameData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageSrc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameGame")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StudiosId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudiosId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Everything.Data.Models.GameStudiosData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Studios");
                });

            modelBuilder.Entity("Everything.Data.Models.GirlData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("ImageSrc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MangaId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("MangaId");

                    b.ToTable("Girls");
                });

            modelBuilder.Entity("Everything.Data.Models.LoadUserData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Coins")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LoadUsers");
                });

            modelBuilder.Entity("Everything.Data.Models.LoadVolumeTestingData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LoadUserDataId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LoadUserDataId");

                    b.ToTable("LoadVolumeTestingMetrics");
                });

            modelBuilder.Entity("Everything.Data.Models.MagazinData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Magazines");
                });

            modelBuilder.Entity("Everything.Data.Models.MangaData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Mangas");
                });

            modelBuilder.Entity("Everything.Data.Models.MetricData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Average")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("LoadUserDataId")
                        .HasColumnType("int");

                    b.Property<int?>("LoadVolumeTestingId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Throughput")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("LoadUserDataId");

                    b.HasIndex("LoadVolumeTestingId");

                    b.ToTable("Metrics");
                });

            modelBuilder.Entity("Everything.Data.Models.ModelData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProducerId")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProducerId");

                    b.HasIndex("TypeId");

                    b.ToTable("Models", (string)null);
                });

            modelBuilder.Entity("Everything.Data.Models.MovieData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("FilmDirectorId")
                        .HasColumnType("int");

                    b.Property<string>("ImageSrc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FilmDirectorId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Everything.Data.Models.ProducerData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Producer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Producers", (string)null);
                });

            modelBuilder.Entity("Everything.Data.Models.Surveys.QuestionData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnswerType")
                        .HasColumnType("int");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Everything.Data.Models.Surveys.StatusData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImagesSrc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("Everything.Data.Models.Surveys.SurveyData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdStatus")
                        .HasColumnType("int");

                    b.Property<int>("SurveyGroupId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SurveyGroupId");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("Everything.Data.Models.Surveys.SurveyGroupData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("СreatorUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("СreatorUserId");

                    b.ToTable("SurveyGroups");
                });

            modelBuilder.Entity("Everything.Data.Models.TypeOfApplianceData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageSrc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypeOfAppliances", (string)null);
                });

            modelBuilder.Entity("Everything.Data.Models.UserData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("AvatarUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Coins")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CakeDataMagazinData", b =>
                {
                    b.HasOne("Everything.Data.Models.CakeData", null)
                        .WithMany()
                        .HasForeignKey("CakesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Everything.Data.Models.MagazinData", null)
                        .WithMany()
                        .HasForeignKey("MagazinsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Everything.Data.Models.AnimeReviewData", b =>
                {
                    b.HasOne("Everything.Data.Models.AnimeData", "Anime")
                        .WithMany("Reviews")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Anime");
                });

            modelBuilder.Entity("Everything.Data.Models.CakeData", b =>
                {
                    b.HasOne("Everything.Data.Models.UserData", "Creator")
                        .WithMany("CreatedCakes")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Everything.Data.Models.CoffeCompanyData", b =>
                {
                    b.HasOne("Everything.Data.Models.CoffeShopActivityData", "TypeOfActivity")
                        .WithMany("CoffeCompanies")
                        .HasForeignKey("TypeOfActivityId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("TypeOfActivity");
                });

            modelBuilder.Entity("Everything.Data.Models.CoffeData", b =>
                {
                    b.HasOne("Everything.Data.Models.BrandData", "Brand")
                        .WithMany("Coffe")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Everything.Data.Models.CoffeCompanyData", "Company")
                        .WithMany("Coffe")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Everything.Data.Models.UserData", "Creator")
                        .WithMany("CreatedCoffe")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Brand");

                    b.Navigation("Company");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Everything.Data.Models.CommentData", b =>
                {
                    b.HasOne("Everything.Data.Models.EcologyData", "Ecology")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Everything.Data.Models.UserData", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Ecology");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Everything.Data.Models.EcologyData", b =>
                {
                    b.HasOne("Everything.Data.Models.UserData", "User")
                        .WithMany("Ecologies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Everything.Data.Models.GameData", b =>
                {
                    b.HasOne("Everything.Data.Models.GameStudiosData", "Studios")
                        .WithMany("Games")
                        .HasForeignKey("StudiosId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Studios");
                });

            modelBuilder.Entity("Everything.Data.Models.GirlData", b =>
                {
                    b.HasOne("Everything.Data.Models.UserData", "Creator")
                        .WithMany("CreatedGirls")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Everything.Data.Models.MangaData", "Manga")
                        .WithMany("Characters")
                        .HasForeignKey("MangaId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Creator");

                    b.Navigation("Manga");
                });

            modelBuilder.Entity("Everything.Data.Models.LoadVolumeTestingData", b =>
                {
                    b.HasOne("Everything.Data.Models.LoadUserData", null)
                        .WithMany("LoadVolumeTestingParts")
                        .HasForeignKey("LoadUserDataId");
                });

            modelBuilder.Entity("Everything.Data.Models.MagazinData", b =>
                {
                    b.HasOne("Everything.Data.Models.UserData", "Creator")
                        .WithMany("CreatedMagazins")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Everything.Data.Models.MangaData", b =>
                {
                    b.HasOne("Everything.Data.Models.UserData", "Author")
                        .WithMany("CreatedMangas")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Everything.Data.Models.MetricData", b =>
                {
                    b.HasOne("Everything.Data.Models.LoadUserData", null)
                        .WithMany("Metrics")
                        .HasForeignKey("LoadUserDataId");

                    b.HasOne("Everything.Data.Models.LoadVolumeTestingData", "LoadVolumeTesting")
                        .WithMany("VolumeMetrics")
                        .HasForeignKey("LoadVolumeTestingId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("LoadVolumeTesting");
                });

            modelBuilder.Entity("Everything.Data.Models.ModelData", b =>
                {
                    b.HasOne("Everything.Data.Models.ProducerData", null)
                        .WithMany()
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Everything.Data.Models.TypeOfApplianceData", null)
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Everything.Data.Models.MovieData", b =>
                {
                    b.HasOne("Everything.Data.Models.FilmDirectorData", "FilmDirector")
                        .WithMany("Movies")
                        .HasForeignKey("FilmDirectorId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("FilmDirector");
                });

            modelBuilder.Entity("Everything.Data.Models.Surveys.QuestionData", b =>
                {
                    b.HasOne("Everything.Data.Models.Surveys.SurveyData", "Survey")
                        .WithMany("Questions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("Everything.Data.Models.Surveys.SurveyData", b =>
                {
                    b.HasOne("Everything.Data.Models.Surveys.SurveyGroupData", "SurveyGroup")
                        .WithMany("Surveys")
                        .HasForeignKey("SurveyGroupId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("SurveyGroup");
                });

            modelBuilder.Entity("Everything.Data.Models.Surveys.SurveyGroupData", b =>
                {
                    b.HasOne("Everything.Data.Models.UserData", "СreatorUser")
                        .WithMany("СreatorSurveyGroups")
                        .HasForeignKey("СreatorUserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("СreatorUser");
                });

            modelBuilder.Entity("Everything.Data.Models.AnimeData", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Everything.Data.Models.BrandData", b =>
                {
                    b.Navigation("Coffe");
                });

            modelBuilder.Entity("Everything.Data.Models.CoffeCompanyData", b =>
                {
                    b.Navigation("Coffe");
                });

            modelBuilder.Entity("Everything.Data.Models.CoffeShopActivityData", b =>
                {
                    b.Navigation("CoffeCompanies");
                });

            modelBuilder.Entity("Everything.Data.Models.EcologyData", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Everything.Data.Models.FilmDirectorData", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("Everything.Data.Models.GameStudiosData", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("Everything.Data.Models.LoadUserData", b =>
                {
                    b.Navigation("LoadVolumeTestingParts");

                    b.Navigation("Metrics");
                });

            modelBuilder.Entity("Everything.Data.Models.LoadVolumeTestingData", b =>
                {
                    b.Navigation("VolumeMetrics");
                });

            modelBuilder.Entity("Everything.Data.Models.MangaData", b =>
                {
                    b.Navigation("Characters");
                });

            modelBuilder.Entity("Everything.Data.Models.Surveys.SurveyData", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Everything.Data.Models.Surveys.SurveyGroupData", b =>
                {
                    b.Navigation("Surveys");
                });

            modelBuilder.Entity("Everything.Data.Models.UserData", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("CreatedCakes");

                    b.Navigation("CreatedCoffe");

                    b.Navigation("CreatedGirls");

                    b.Navigation("CreatedMagazins");

                    b.Navigation("CreatedMangas");

                    b.Navigation("Ecologies");

                    b.Navigation("СreatorSurveyGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
