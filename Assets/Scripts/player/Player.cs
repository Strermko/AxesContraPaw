using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace player
{
    public class Player : SerializedMonoBehaviour
    {
        [OdinSerialize] private PlayerState _playerState;

        public PlayerState PlayerState
        {
            get => _playerState;
            set => _playerState = value;
        }
    }
}