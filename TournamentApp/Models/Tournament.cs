using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TournamentApp.Models.GameStrategies;

namespace TournamentApp.Models
{
    public class Tournament
    {
        public int TournamentId { get; set; }

        [Required(ErrorMessage = "Team name is required.")]
        [StringLength(20)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sport {  get; set; }
        public List<Team> Teams { get; set; } = new List<Team>();
        public int GameStrategyId { get; set; }
        public GameStrategy GameStrategy { get; set; }
        public bool Started { get; set; } = false;
        public List<Match> Matches { get; set; } = new List<Match>();

        public void ResetTournament()
        {
            Started = false;
            Matches = null;
            foreach (var team in Teams)
            {
                team.ResetTeam();
            }
        }
    }

}
