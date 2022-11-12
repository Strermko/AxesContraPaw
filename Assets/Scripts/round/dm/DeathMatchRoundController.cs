using System.Collections.Generic;
using player;
using team;
using UnityEngine.Events;

namespace round
{
    public class DeathMatchRoundController : IRoundController
    {
        private DeathMatchRoundState _roundState;
        private RoundWonEvent _roundWonEvent;

        public DeathMatchRoundController(UnityAction<RoundState> runWonListener)
        {
            _roundState = new DeathMatchRoundState();
            _roundState.DeadPlayers = new List<Player>();
            _roundWonEvent = new RoundWonEvent();
            _roundWonEvent.AddListener(runWonListener);
        }

        public void PlayRound(List<Team> teams)
        {
            if (CheckTimeElapsed())
            {
                _roundWonEvent.Invoke(calculateWinner(teams));
            }

            UpdateState(teams);

            foreach (var team in teams)
            {
                if (CheckWholeTeamIsDead(team))
                {
                    _roundWonEvent.Invoke(calculateWinner(teams));
                }
            }
        }

        private void UpdateState(List<Team> teams)
        {
            UpdateDeadList(teams);
        }

        private void UpdateDeadList(List<Team> teams)
        {
            foreach (var team in teams)
            {
                foreach (var player in team.Players)
                {
                    if (player.PlayerState.HealthPoints <= 0)
                    {
                        _roundState.DeadPlayers.Add(player);
                    }
                }
            }
        }

        public void Reset(List<Team> teams)
        {
            foreach (var team in teams)
            {
                ResetPlayersHealth(team.Players);
            }
        }

        private void ResetPlayersHealth(List<Player> players)
        {
            foreach (var player in players)
            {
                player.PlayerState.HealthPoints = player.PlayerState.MaxHp;
            }
        }

        private RoundState calculateWinner(List<Team> teams)
        {
            Team bestTeam = null;
            int bestScore = 0;
            Dictionary<Team, int> scoreDict = CalculateScore(teams);
            foreach (var team in teams)
            {
                if (scoreDict[team] > bestScore)
                {
                    bestScore = scoreDict[team];
                    bestTeam = team;
                }
            }

            RoundState state = new RoundState();
            state.WinningTeam = bestTeam;
            return state;
        }

        private Dictionary<Team, int> CalculateScore(List<Team> teams)
        {
            Dictionary<Team, int> teamScoreDictionary = new Dictionary<Team, int>();
            foreach (var team in teams)
            {
                teamScoreDictionary.Add(team, SumUpHealthPoints(team));
            }

            return teamScoreDictionary;
        }

        private bool CheckWholeTeamIsDead(Team team)
        {
            foreach (var player in team.Players)
            {
                if (player.PlayerState.HealthPoints > 0)
                {
                    return false;
                }
            }

            return true;
        }

        private int SumUpHealthPoints(Team team)
        {
            int sum = 0;
            foreach (Player player in team.Players)
            {
                sum += player.PlayerState.HealthPoints;
            }

            return sum;
        }

        private bool CheckTimeElapsed()
        {
            // TODO: implement this
            return false;
        }
    }
}