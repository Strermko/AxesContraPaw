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
        private Timer _timer;
        private float _roundTimeInSeconds;

        public DeathMatchRoundController(UnityAction<RoundState> runWonListener, Timer timer, float roundTimeInSeconds)
        {
            _roundState = new DeathMatchRoundState();
            _roundState.DeadPlayers = new List<Player>();
            _roundWonEvent = new RoundWonEvent();
            _roundWonEvent.AddListener(runWonListener);
            _timer = timer;
            _roundTimeInSeconds = roundTimeInSeconds;
        }

        public void PlayRound(List<Team> teams)
        {
            if (!_timer.TimerIsRunning)
            {
                _timer.TimeRemainingInSeconds = _roundTimeInSeconds;
                _timer.StartTimer();
            }

            if (CheckTimeElapsed())
            {
                _roundWonEvent.Invoke(CalculateWinner(teams));
                _timer.StopTimer();
            }

            UpdateState(teams);

            foreach (var team in teams)
            {
                if (CheckWholeTeamIsDead(team))
                {
                    _roundWonEvent.Invoke(CalculateWinner(teams));
                    _timer.StopTimer();
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

        private RoundState CalculateWinner(List<Team> teams)
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
            return _timer.TimeRemainingInSeconds <= 0f;
        }
    }
}