namespace TournamentApp.Models;

public class Match
{
	public int MatchId { get; set; }
	public int? T1Id { get; set; }
	public Team T1 { get; set; }
	public int? T2Id { get; set; }
	public Team T2 { get; set; }
	public int Score1 { get; set; } = 0;
	public int Score2 { get; set; } = 0;
	public Team Winner { get; set; }
	public int? WinnerId { get; set; }
	public bool IsWinner { get; set; } = false;
	public int? PrevMatch1Id { get; set; }
	public Match PrevMatch1 { get; set; }
	public int? PrevMatch2Id { get; set; }
	public Match PrevMatch2 { get; set; }
	public int Round { get; set; }
	public int TourId { get; set; }
	public Tournament Tournament { get; set; }
	public bool IsFinal { get; set; }
	public bool IsHidden { get; set; } = false;

	public void AddTeamStats()
	{
		T1.Points += Score1;
		T2.Points += Score2;
		if (!IsWinner)
		{
			return;
		}
		if (Winner == T1)
		{
			T1.Wins++;
			T2.Losses++;
		}
		else
		{
			T2.Wins++;
			T1.Losses++;
		}
    }

}
