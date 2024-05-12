using UnityEngine;

namespace Game.Spawners
{
    public abstract class Spawner : MonoBehaviour
    {
        [SerializeField] private SpawnerPrefabPool _prefabPool;

        [SerializeField][Range(0,1f)] private float _spawnChance;
        [SerializeField] private bool _spawnOnStart;

        protected SpawnerPrefabPool PrefabPool => _prefabPool;
        protected float SpawnChance => _spawnChance;

        private void Start()
        {
            if(_spawnOnStart)
                Spawn();
        }
        public abstract void Spawn();
    }
}

