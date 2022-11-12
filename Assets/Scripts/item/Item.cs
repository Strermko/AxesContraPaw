using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace item
{
    public struct Item
    {
        public string Name { get; set; }

        public Dictionary<ItemStatistics, int> DefendStats { get; set; }

        public Dictionary<ItemStatistics, int> AttackStats { get; set; }

        public GameObject Prefab { get; set; }

        public Image Avatar { get; set; }
    }
}