using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private RoomType _roomType;

    [SerializeField] private Transform _startConnector;

    [SerializeField] private List<ChunkConnector> _chunkConnectors;

    [SerializeField] private GameObject _sturcture;

    private Vector3 _roomSize;
    private Vector3 _roomCenter;

    public RoomType RoomType => _roomType;
    public Transform StartConnector => _startConnector;
    public List<ChunkConnector> ChunkConnectors => _chunkConnectors;
    public Vector3 RoomSize => _roomSize;
    public Vector3 RoomCenter => _roomCenter;


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

        CalculateRoomSizeAndCenter();
    }

    private void CalculateRoomSizeAndCenter()
    {
        Renderer renderer = _sturcture.GetComponent<Renderer>();
        _roomSize = renderer.bounds.size;
        _roomCenter = renderer.bounds.center;
    }
}
