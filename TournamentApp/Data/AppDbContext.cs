using Microsoft.EntityFrameworkCore;
using TournamentApp.Models;
using TournamentApp.Models.GameStrategies;

namespace TournamentApp.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Tournament> Tournaments { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<Match> MatchTests { get; set; }
        public DbSet<GameStrategy> GameStrategies { get; set; }
		public DbSet<SingleElimination> SingleEliminations { get; set; }
		public DbSet<RoundRobin> RoundRobins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Tournament>()
				.HasMany(t => t.Teams)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tournament>()
				.HasOne(t => t.GameStrategy)
				.WithMany()
				.HasForeignKey(t => t.GameStrategyId)
				.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tournament>()
				.HasMany(t => t.Matches)
				.WithOne(m => m.Tournament)
				.HasForeignKey(m => m.TourId);

            modelBuilder.Entity<Match>()
				.HasOne(m => m.PrevMatch1)
				.WithMany()
				.HasForeignKey(m => m.PrevMatch1Id)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Match>()
				.HasOne(m => m.PrevMatch2)
				.WithMany()
				.HasForeignKey(m => m.PrevMatch2Id)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Match>()
				.HasOne(m => m.Winner)
				.WithMany()
				.HasForeignKey(m => m.WinnerId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Match>()
				.HasOne(m => m.T1)
				.WithMany()
				.HasForeignKey(m => m.T1Id)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Match>()
				.HasOne(m => m.T2)
				.WithMany()
				.HasForeignKey(m => m.T2Id)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Match>()
				.HasOne(m => m.Tournament)
				.WithMany()
				.HasForeignKey(m => m.TourId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<GameStrategy>()
				.HasDiscriminator<string>("GameStrategyType")
				.HasValue<SingleElimination>("Brackets")
				.HasValue<GameStrategy>("RoundRobin");


			modelBuilder.Entity<RoundRobin>().HasData(
				new RoundRobin { Id = 1, Name = "Round Robin" }
);

			modelBuilder.Entity<SingleElimination>().HasData(
				new SingleElimination { Id = 2, Name = "Brackets" }
			);


		}
    }
}
