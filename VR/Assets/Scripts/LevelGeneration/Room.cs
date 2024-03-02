using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelGenaration
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private RoomType _roomType;

        [SerializeField] private Transform _startConnector;

        [SerializeField] private List<ChunkConnector> _chunkConnectors;

        [SerializeField] private GameObject _sturcture;

        [SerializeField] private Vector2 _roomSize;
        [SerializeField] private Vector3 _roomCenter;
        [SerializeField] private Vector3 _roomOffsetFromPivot;

        private Room _previousConnectedRoom = null;

        public RoomType RoomType => _roomType;
        public Transform StartConnector => _startConnector;
        public List<ChunkConnector> ChunkConnectors => _chunkConnectors;
        public Vector2 RoomSize => _roomSize;
        public Vector3 RoomCenter => _roomCenter;
        public Vector3 RoomOffsetFromPivot => _roomOffsetFromPivot;
        public Room PreviousConnectedRoom => _previousConnectedRoom;

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

        public void SetPreviousRoom(Room room)
        {
            if (_previousConnectedRoom == null)
                _previousConnectedRoom = room;
            else
                throw new System.SystemException("Room is already setted");
        }

        public void CalculateRoomSizeAndCenter()
        {
            Renderer renderer = _sturcture.GetComponent<Renderer>();
            _roomSize.Set(renderer.bounds.size.x, renderer.bounds.size.z);
            _roomCenter = renderer.bounds.center;
            _roomOffsetFromPivot = transform.position - _roomCenter;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(_roomCenter, new Vector3(_roomSize.x, 5f, _roomSize.y));
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_roomCenter, 0.1f);
        }
    }
}
