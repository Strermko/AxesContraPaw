using System.Collections.Generic;
using team;

namespace round
{
    public interface IRoundController
    {
        void PlayRound(List<Team> teams);
        void Reset(List<Team> teams);
    }
}