using UnityEngine;

namespace Game.Spawners
{
    public class ItemSpawner : Spawner
    {
        public override void Spawn()
        {
            float randomPercent = Random.Range(0f, 1f);

            if (randomPercent < SpawnChance)
                return;

            int poolCount = PrefabPool.PoolList.Count;

            Instantiate(PrefabPool.PoolList[Random.Range(0, poolCount - 1)], transform.position, Quaternion.identity);
        }
    }
}