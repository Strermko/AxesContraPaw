using UnityEngine.Events;

namespace round
{
    [System.Serializable]
    public class RoundWonEvent : UnityEvent<RoundState>
    {
    }
}