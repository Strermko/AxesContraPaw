using System.Collections.Generic;
using player;
using round;
using Sirenix.OdinInspector;
using team;
using UnityEngine;

namespace match
{
    public class DeathMatchController : SerializedMonoBehaviour
    {
        [SerializeField] private float initialTime;
        [SerializeField] private List<Team> teams;
        [SerializeField] private int maxPoints;
        [SerializeField] private Timer timer;
        private DeathMatchState _state;
        private IRoundController _roundController;

        private void Awake()
        {
            
            _state = new DeathMatchState
            {
                TeamScores = new Dictionary<Team, int>()
            };
            foreach (var team in teams)
            {
                _state.TeamScores.Add(team, 0);
            }
        }

        private void Start()
        {
            if (maxPoints == 0)
            {
                Debug.LogError("MAX POINTS NOT SET !");
            }

            if (teams == null)
            {
                Debug.LogError("TEAMS NOT SET !");
            }

            if (initialTime == 0)
            {
                Debug.LogError("INITIAL TIME NOT SET !");
            }

            if (timer == null)
            {
                Debug.LogError("TIMER NOT SET !");
            }
            
            _roundController = new DeathMatchRoundController(HandleRoundWon, timer,initialTime);
        }

        private void Update()
        {
            Play();
        }

        private void Play()
        {
            // player picks items he will be using during match
            // round starts
            //time
            _roundController.PlayRound(teams);
        }

        private void HandleRoundWon(RoundState roundState)
        {
            _state.TeamScores[roundState.WinningTeam] += 1;
            Debug.Log("Handling won round team: ", roundState.WinningTeam);
            _roundController.Reset(teams);
            // TODO start next round reset position of teamsForRound etc
        }
    }
}