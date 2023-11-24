using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum RoomType
    {
        StartRoom,
        Connector,
        EndRoom
    }

    [SerializeField] private RoomType _roomType;

    [SerializeField] private Transform _startConnector;

    [SerializeField] private List<ChunkConnector> _chunkConnectors = new List<ChunkConnector>();

    public RoomType Roomtype => _roomType;

    public Transform StartConnector => _startConnector;

    public List<ChunkConnector> ChunkConnectors => _chunkConnectors;


    private void Start()
    {
        if(_chunkConnectors.Count == 0)
        {
            List<ChunkConnector> levelChunks = GetComponentsInChildren<ChunkConnector>().ToList();

            if (levelChunks == null)
                throw new System.NullReferenceException("There is no connectors in prefab");

            foreach(ChunkConnector levelChunk in levelChunks)
                _chunkConnectors.Add(levelChunk);
        }
    }
}
