using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<Room> _levelChunks = new List<Room>();

    [SerializeField] private ChunkConnector _startConnector;

    //Parameters
    [SerializeField] private int _minRoomCount;
    [SerializeField] private int _maxRoomCount;

    private List<Room> _createdRooms = new List<Room>();

    private void OnValidate()
    {
        if(_minRoomCount > _maxRoomCount)
            _minRoomCount = _maxRoomCount - 1;
    }

    private void Start()
    {
        StartGeneration();
    }

    private void StartGeneration()
    {
        Room randomRoom = GetRandomRoom();
        Room createdRoom = _startConnector.ConnectNewRoom(randomRoom);
    }

    private Room GetRandomRoom()
    {
        return _levelChunks[Random.Range(0, _levelChunks.Count)];
    }
}
