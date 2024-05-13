using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeycardSpawnHandler : MonoBehaviour 
{
    private List<KeycardReader> _keycardReaders = new List<KeycardReader>();
    private List<KeycardSpawnPoint> _keycardSpawners = new List<KeycardSpawnPoint>();

    private void Start()
    {
        _keycardReaders = FindObjectsOfType<KeycardReader>().ToList();
        _keycardSpawners = FindObjectsOfType<KeycardSpawnPoint>().ToList();

        int readerIndex = 0;
        for(int i = 0; i < _keycardSpawners.Count && readerIndex < _keycardReaders.Count; i++)
        {
            int randomSpawnerIndex = Random.Range(0, _keycardSpawners.Count);

            KeycardSpawnPoint spawnPoint = _keycardSpawners[randomSpawnerIndex];

            if (spawnPoint.IsUsed)
                continue;

            _keycardSpawners[randomSpawnerIndex].Spawn(_keycardReaders[readerIndex].Type);
            readerIndex++;
        }
    }
}