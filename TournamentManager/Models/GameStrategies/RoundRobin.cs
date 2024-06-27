﻿using System;
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

        return matches;
    }

	public override void UpdateMatches(List<Match> matches)
	{
        return;
	}
}