using DefaultNamespace;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace player
{
    public class PlayerState : SerializedMonoBehaviour
    {
        [InfoBox("Nick for player")]
        [ShowInInspector]
        [OdinSerialize]
        public string Nick { get; set; }

        [InfoBox("Image avatar")]
        [ShowInInspector]
        [OdinSerialize]
        public Image Avatar { get; set; }

        [InfoBox("Players prefab")]
        [ShowInInspector]
        [OdinSerialize]
        public GameObject Prefab { get; set; }

        [InfoBox("HP")]
        [ShowInInspector, PropertyRange(0, 100)]
        [OdinSerialize]
        public int HealthPoints { get; set; }

        [InfoBox("Players MAX HP")]
        [ShowInInspector]
        [OdinSerialize]
        public int MaxHp { get; set; }

        [InfoBox("Players inventory")]
        [ShowInInspector]
        [OdinSerialize]
        public Inventory Inventory { get; set; }
    }
}