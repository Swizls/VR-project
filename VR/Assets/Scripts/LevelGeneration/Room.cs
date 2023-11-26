using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private RoomType _roomType;

    [SerializeField] private Transform _startConnector;

    [SerializeField] private List<ChunkConnector> _chunkConnectors;

    [SerializeField] private GameObject _sturcture;

    [SerializeField] private Vector2Int _roomSize;
    [SerializeField] private Vector3 _roomCenter;
    [SerializeField] private Vector3 _roomOffsetFromConnector;

    public RoomType RoomType => _roomType;
    public Transform StartConnector => _startConnector;
    public List<ChunkConnector> ChunkConnectors => _chunkConnectors;
    public Vector2Int RoomSize => _roomSize;
    public Vector3 RoomCenter => _roomCenter;
    public Vector3 RoomOffsetFromConnector => _roomOffsetFromConnector;


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

    public void CalculateRoomSizeAndCenter()
    {
        Renderer renderer = _sturcture.GetComponent<Renderer>();
        _roomSize = new Vector2Int((int)renderer.bounds.size.x + 1, (int)renderer.bounds.size.z + 1);
        _roomCenter = renderer.bounds.center;
        _roomOffsetFromConnector = _startConnector.position - _roomCenter;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(_roomCenter, new Vector3(_roomSize.x, 5f, _roomSize.y));
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_roomCenter, 0.1f);
    }
}
