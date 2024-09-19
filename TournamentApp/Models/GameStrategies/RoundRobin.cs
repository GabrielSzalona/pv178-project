using System;
using System.Collections.Generic;
using TournamentApp.Pages.TournamentPages;

namespace TournamentApp.Models.GameStrategies;

public class RoundRobin : GameStrategy
{
    public RoundRobin()
    {
        Name = "RoundRobin";
    }

    public override List<Match> CreateMatches(List<Team> teams, int tourId)
    {
        var matches = new List<Match>();
        for (int i = 0; i < teams.Count; i++)
        {
            for (int j = i + 1; j < teams.Count; j++)
            {
                matches.Add(new Match { T1 = teams[i], T2 = teams[j], TourId = tourId });   
            }
        }

        Shuffle(matches);
        return matches;
    }

    private void Shuffle(List<Match> matches)
    {
		Random random = new Random();
		int n = matches.Count;
		for (int i = n - 1; i > 0; i--)
		{
			int j = random.Next(0, i + 1);
			Match temp = matches[i];
			matches[i] = matches[j];
			matches[j] = temp;
		}
	}

	public override void UpdateMatches(List<Match> matches)
	{
        return;
	}
}
