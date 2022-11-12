using System.Collections.Generic;
using team;

namespace round
{
    public struct DeathMatchState
    {
        public long TimeLeft { get; set; }
        public Dictionary<Team, int> TeamScores { get; set; }
    }
}