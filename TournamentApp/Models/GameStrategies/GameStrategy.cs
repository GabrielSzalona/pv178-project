using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TournamentApp.Models.GameStrategies;

public abstract class GameStrategy
{
    public int Id { get; set; }
    public string Name { get; set; }
    public abstract List<Match> CreateMatches(List<Team> teams, int tourId);
    public abstract void UpdateMatches(List<Match> matches);
}
