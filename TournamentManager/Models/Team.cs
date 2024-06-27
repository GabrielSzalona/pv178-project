using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TournamentApp.Models
{
    public class Team
    {
        public int TeamId { get; set; }

        [Required(ErrorMessage = "Team name is required.")]
        [StringLength(15, ErrorMessage = "Team name must be less than 20 characters.")]
        public string Name { get; set; }
        public List<string> Members { get; set; } = new List<string>();
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Points { get; set; }
        public void ResetTeam()
        {
            Wins = 0;
            Losses = 0;
            Points = 0;
        }
    }
}
