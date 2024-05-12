using System.Collections.Generic;
using UnityEngine;

namespace Game.Spawners
{
    [CreateAssetMenu(fileName = "Default object pool", menuName = "Spawners/Object pool")]
    public class SpawnerPrefabPool : ScriptableObject
    {
        [SerializeField] private List<GameObject> _poolList;

        public List<GameObject> PoolList => _poolList;
    }
}