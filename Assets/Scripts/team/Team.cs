using System.Collections.Generic;
using player;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace team
{
    public class Team : SerializedMonoBehaviour
    {
        [ShowInInspector]
        [OdinSerialize]
        public string Name { get; set; }

        [ShowInInspector]
        [OdinSerialize]
        public List<Player> Players { get; set; }
    }
}