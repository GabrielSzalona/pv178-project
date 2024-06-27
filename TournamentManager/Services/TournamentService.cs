using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentApp.Data;
using TournamentApp.Models;
using TournamentApp.Models.GameStrategies;
using TournamentApp.Pages.TournamentPages;

namespace TournamentApp.Services
{
    public class TournamentService: 
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public static Dictionary<string, Type> strategyRoutes = new()
        {
            { new SingleElimination().Name, Type.GetType($"TournamentApp.Pages.StrategyPages.SingleElimination") },
            { new RoundRobin().Name, Type.GetType($"TournamentApp.Pages.StrategyPages.RoundRobin") }
        };


        public static Dictionary<string, Type> stringToStrategyMap = new()
        {
            { new SingleElimination().Name, typeof(SingleElimination)},
            { new RoundRobin().Name, typeof(RoundRobin)},
        };


        public TournamentService(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Tournament>> GetTournamentsAsync()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Tournaments
                    .Include(t => t.Teams)
                    .Include(t => t.Matches)
                    .ToListAsync();
            }
        }

        public async Task<Tournament> GetTournamentByIdAsync(int tournamentId)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Tournaments
                    .Include(t => t.Teams)
                    .Include(t => t.GameStrategy)
                    .Include(t => t.Matches)
                    .FirstOrDefaultAsync(t => t.TournamentId == tournamentId);
            }
        }

        public async Task<bool> AddTournamentAsync(Tournament tournament)
        {
            if (await IsDuplicitName(tournament))
            {
                return false;
            }

            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                dbContext.Tournaments.Add(tournament);
                await dbContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> UpdateWithoutMatchesAsync(Tournament tournament)
        {

            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                tournament.Matches = null;
                dbContext.Tournaments.Update(tournament);
                await dbContext.SaveChangesAsync();
                return true;
            }
        }


        public async Task ResetTournament(Tournament tournament)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                tournament.Started = false;
                dbContext.MatchTests.RemoveRange(tournament.Matches);

                tournament.ResetTournament();

                foreach (var team in tournament.Teams)
                {
                    team.ResetTeam();
                }

                dbContext.Tournaments.Update(tournament);
                await dbContext.SaveChangesAsync();
            }

        }

        public async Task UpdateTournamentAsync(Tournament tournament)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                dbContext.Tournaments.Update(tournament);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteTournamentAsync(int tournamentId)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var tournament = await dbContext.Tournaments
                    .Include(t => t.Teams)
                    .Include(t => t.Matches)
                    .FirstOrDefaultAsync(t => t.TournamentId == tournamentId);

                if (tournament != null)
                {
                    dbContext.Teams.RemoveRange(tournament.Teams);
                    dbContext.MatchTests.RemoveRange(tournament.Matches);
                    dbContext.Tournaments.Remove(tournament);

                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteTeamAsync(Tournament tournament, Team team)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                dbContext.Teams.Remove(team);
                tournament.Teams.Remove(team);
                dbContext.Tournaments.Update(tournament);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> IsDuplicitName(Tournament tournament)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Tournaments.FirstOrDefaultAsync(t => t.Name.Equals(tournament.Name)) != null;
            }
        }
    }

}
