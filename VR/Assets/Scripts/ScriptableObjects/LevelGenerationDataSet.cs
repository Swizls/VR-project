using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default level generation data set", menuName = "Generation data sets/Default level generation data set")]
public class LevelGenerationDataSet : ScriptableObject
{
    [SerializeField] private List<GameObject> _levelChunks;

    public List<GameObject> LevelChunks => _levelChunks;
}
