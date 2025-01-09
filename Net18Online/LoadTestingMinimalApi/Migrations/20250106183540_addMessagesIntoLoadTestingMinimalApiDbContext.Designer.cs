﻿// <auto-generated />
using System;
using LoadTestingMinimalApi.DBstuff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LoadTestingMinimalApi.Migrations
{
    [DbContext(typeof(ChatDbContext))]
    [Migration("20250106183540_addMessagesIntoLoadTestingMinimalApiDbContext")]
    partial class addMessagesIntoLoadTestingMinimalApiDbContext
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LoadTestingMinimalApi.DBstuff.DataModels.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("LoadTestingMinimalApi.DBstuff.DataModels.MetricData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Average")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("CanDelete")
                        .HasColumnType("bit");

                    b.Property<string>("CreatorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsLiked")
                        .HasColumnType("bit");

                    b.Property<int>("LikeCount")
                        .HasColumnType("int");

                    b.Property<string>("LoadVolumeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Throughput")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Metrics");
                });
#pragma warning restore 612, 618
        }
    }
}
