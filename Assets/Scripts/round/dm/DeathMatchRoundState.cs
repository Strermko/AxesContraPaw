using System.Collections.Generic;
using player;
using team;

namespace round
{
    public class DeathMatchRoundState
    {
        public List<Player> DeadPlayers { get; set; }

        public Team CurrentTeam { get; set; }

        public long TimeLeft { get; set; }
    }
}