﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TournamentApp.Data;

#nullable disable

namespace TournamentApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TournamentApp.Models.GameStrategies.GameStrategy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("GameStrategyType")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GameStrategies");

                    b.HasDiscriminator<string>("GameStrategyType").HasValue("RoundRobin");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("TournamentApp.Models.Match", b =>
                {
                    b.Property<int>("MatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MatchId"));

                    b.Property<bool>("IsFinal")
                        .HasColumnType("bit");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("bit");

                    b.Property<bool>("IsWinner")
                        .HasColumnType("bit");

                    b.Property<int?>("PrevMatch1Id")
                        .HasColumnType("int");

                    b.Property<int?>("PrevMatch2Id")
                        .HasColumnType("int");

                    b.Property<int>("Round")
                        .HasColumnType("int");

                    b.Property<int>("Score1")
                        .HasColumnType("int");

                    b.Property<int>("Score2")
                        .HasColumnType("int");

                    b.Property<int?>("T1Id")
                        .HasColumnType("int");

                    b.Property<int?>("T2Id")
                        .HasColumnType("int");

                    b.Property<int>("TourId")
                        .HasColumnType("int");

                    b.Property<int?>("TournamentId")
                        .HasColumnType("int");

                    b.Property<int?>("WinnerId")
                        .HasColumnType("int");

                    b.HasKey("MatchId");

                    b.HasIndex("PrevMatch1Id");

                    b.HasIndex("PrevMatch2Id");

                    b.HasIndex("T1Id");

                    b.HasIndex("T2Id");

                    b.HasIndex("TourId");

                    b.HasIndex("TournamentId");

                    b.HasIndex("WinnerId");

                    b.ToTable("MatchTests");
                });

            modelBuilder.Entity("TournamentApp.Models.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeamId"));

                    b.Property<int>("Losses")
                        .HasColumnType("int");

                    b.Property<string>("Members")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<int?>("TournamentId")
                        .HasColumnType("int");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("TeamId");

                    b.HasIndex("TournamentId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("TournamentApp.Models.Tournament", b =>
                {
                    b.Property<int>("TournamentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TournamentId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GameStrategyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Sport")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Started")
                        .HasColumnType("bit");

                    b.HasKey("TournamentId");

                    b.HasIndex("GameStrategyId");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("TournamentApp.Models.GameStrategies.RoundRobin", b =>
                {
                    b.HasBaseType("TournamentApp.Models.GameStrategies.GameStrategy");

                    b.HasDiscriminator().HasValue("RoundRobin");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Round Robin"
                        });
                });

            modelBuilder.Entity("TournamentApp.Models.GameStrategies.SingleElimination", b =>
                {
                    b.HasBaseType("TournamentApp.Models.GameStrategies.GameStrategy");

                    b.HasDiscriminator().HasValue("Brackets");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Name = "Brackets"
                        });
                });

            modelBuilder.Entity("TournamentApp.Models.Match", b =>
                {
                    b.HasOne("TournamentApp.Models.Match", "PrevMatch1")
                        .WithMany()
                        .HasForeignKey("PrevMatch1Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TournamentApp.Models.Match", "PrevMatch2")
                        .WithMany()
                        .HasForeignKey("PrevMatch2Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TournamentApp.Models.Team", "T1")
                        .WithMany()
                        .HasForeignKey("T1Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TournamentApp.Models.Team", "T2")
                        .WithMany()
                        .HasForeignKey("T2Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TournamentApp.Models.Tournament", "Tournament")
                        .WithMany()
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TournamentApp.Models.Tournament", null)
                        .WithMany("Matches")
                        .HasForeignKey("TournamentId");

                    b.HasOne("TournamentApp.Models.Team", "Winner")
                        .WithMany()
                        .HasForeignKey("WinnerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("PrevMatch1");

                    b.Navigation("PrevMatch2");

                    b.Navigation("T1");

                    b.Navigation("T2");

                    b.Navigation("Tournament");

                    b.Navigation("Winner");
                });

            modelBuilder.Entity("TournamentApp.Models.Team", b =>
                {
                    b.HasOne("TournamentApp.Models.Tournament", null)
                        .WithMany("Teams")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TournamentApp.Models.Tournament", b =>
                {
                    b.HasOne("TournamentApp.Models.GameStrategies.GameStrategy", "GameStrategy")
                        .WithMany()
                        .HasForeignKey("GameStrategyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("GameStrategy");
                });

            modelBuilder.Entity("TournamentApp.Models.Tournament", b =>
                {
                    b.Navigation("Matches");

                    b.Navigation("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
