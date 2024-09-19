using System;
using System.Collections.Generic;
using System.Linq;

namespace TournamentApp.Models.GameStrategies;

public class SingleElimination : GameStrategy
{
    public SingleElimination()
    {
        Name = "SingleElimination";
    }

    public override List<Match> CreateMatches(List<Team> teams, int tourId)
	{
        var matches = new List<Match>();
        
        var playersCount = teams.Count;
        var upperPower = Math.Ceiling(Math.Log2(playersCount));
        var countNodesUpperBound = (int)Math.Pow(2, upperPower);

        // Calculates the number that is a power of 2 and lower than numPlayers.
        var countNodesLowerBound = countNodesUpperBound / 2;

        // num of matches will be upper bound - 1
        for (int i = 0; i < countNodesUpperBound - 1; i++)
        {
            matches.Add(new Match()
            {
                TourId = tourId
            });
        }

        matches[0].Round = (int) upperPower;
        matches[0].IsFinal = true;

        LinkPrevMatches(matches);

        matches.Reverse();

        FillPlayers(countNodesLowerBound, teams, matches);

        UpdateMatches(matches);
        return matches;
    }

    public static void FillPlayers(int countNodesLowerBound, List<Team> teams, List<Match> matches)
    {
        int countHidden = teams.Count - countNodesLowerBound;
        int j = 0;

        for (int i = 0; i < countNodesLowerBound; i++)
        {
            if (i < countHidden)
            {
                matches[i].T1 = teams[j];
                matches[i].T2 = teams[j + 1];
                j += 2;
            }
            else
            {
                matches[i].IsHidden = matches[i].IsWinner = true;
                matches[i].Winner = teams[j];
                j++;
            }
        }
    }

    public static void LinkPrevMatches(List<Match> matches)
    {
        for (int i = 0; i < matches.Count; i++)
        {
            var idx1 = (i * 2) + 1;
            var idx2 = (i * 2) + 2;
            if (idx2 < matches.Count)
            {
                matches[i].PrevMatch1 = matches[idx2];
                matches[idx2].Round = matches[i].Round - 1;
            }
            if (idx1 < matches.Count)
            {
                matches[i].PrevMatch2 = matches[idx1];
                matches[idx1].Round = matches[i].Round - 1;
            }
        }
    }


    public override void UpdateMatches(List<Match> matches)
	{
		foreach (var match in matches.OrderBy(x => x.Round))
		{
			UpdateTeams(match);
		}
	}

	private void UpdateTeams(Match match)
	{
		if (match.Round == 1)
		{
			return;
		}

		if (match.PrevMatch1.Winner != match.T1 || match.PrevMatch2.Winner != match.T2)
		{
			match.Winner = null;
		}
		match.T1 = match.PrevMatch1.Winner;
		match.T2 = match.PrevMatch2.Winner;
	}
}
