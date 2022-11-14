using System.Collections.Generic;
using team;

namespace round
{
    public struct DeathMatchState
    {
        public Dictionary<Team, int> TeamScores { get; set; }
    }
}