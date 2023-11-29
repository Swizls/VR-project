using LevelGenaration;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private RoomCollection _roomCollection;

    [SerializeField] private ChunkConnector _startRoomConnector;

    [SerializeField] private int _minRoomCount;
    [SerializeField] private int _maxRoomCount;

    [SerializeField] private bool _generateLevelOnStart;

    [Space]
    [Header("Grid settings")]

    [SerializeField] private int _gridHeight;
    [SerializeField] private int _gridWidth;
    [SerializeField] private int _cellSize;

    private LevelGrid _grid;
    private RoomSelector _roomSelector;

    private List<Room> _createdRooms = new List<Room>();
    private List<ChunkConnector> _availableConnectors = new List<ChunkConnector>();

    public int MinRoomCount => _minRoomCount;
    public int MaxRoomCount => _maxRoomCount;
    public LevelGrid Grid => _grid;
    public List<ChunkConnector> AvailableConnectors => _availableConnectors;
    public List<Room> CreatedRooms => _createdRooms;

    private void OnValidate()
    {
        if(_maxRoomCount < _minRoomCount)
            _maxRoomCount = _minRoomCount - 1;
    }

    private void Start()
    {
        Initialize();
        if (_generateLevelOnStart)
            StartGeneration();
    }

    private void StartGeneration()
    {
        Vector3 startRoomSpawnPosition = _grid.GetRandomPositionOnGrid();

        Room startRoom = Instantiate(_roomCollection.StartRoom, startRoomSpawnPosition, Quaternion.identity);

        startRoom.transform.position -= _roomCollection.StartRoom.RoomCenter;

        _createdRooms.Add(startRoom);

        _grid.TrySetOccupationOnGrid(startRoomSpawnPosition, startRoom);

        StartCoroutine(GenerateLevel(startRoom));
    }

    private void Initialize()
    {
        _roomSelector = new RoomSelector(this, _roomCollection);
        _grid = new LevelGrid(_gridHeight, _gridWidth, _cellSize);
    }

    private IEnumerator GenerateLevel(Room previousRoom)
    {
        while(_createdRooms.Count < _maxRoomCount)
        {
            yield return new WaitForSeconds(1);
            ChunkConnector chunkConnector = GetAvailableConnector(previousRoom);

            if (chunkConnector == null && _availableConnectors.Count != 0)
                chunkConnector = _availableConnectors[0];

            GameObject nextRoom = _roomSelector.SelectRoom(chunkConnector);

            Room newRoom = CreateRoom(chunkConnector, nextRoom);

            if (_createdRooms.Count < _maxRoomCount)
                GenerateLevel(newRoom);
            else
                FillEmptySpace();
        }
    }

    private void UpdateAvailabeConnectors()
    {
        List<ChunkConnector> connectors = new List<ChunkConnector>();
        foreach (Room room in _createdRooms)
        {
            if (room.ChunkConnectors.Count == 0)
                continue;

            foreach (ChunkConnector connector in room.ChunkConnectors)
            {
                if (!connector.IsConnected)
                    connectors.Add(connector);
            }
        }
        _availableConnectors = connectors;
    }

    private Room CreateRoom(ChunkConnector connector, GameObject newRoom)
    {
        Room createdRoom = connector.ConnectNewRoom(newRoom);
        _grid.TrySetOccupationOnGrid(createdRoom.StartConnector.position, createdRoom, connector.Direction);

        _createdRooms.Add(createdRoom);
        UpdateAvailabeConnectors();

        return createdRoom;
    }

    public void FillEmptySpace()
    {
        if(_availableConnectors.Count == 0) 
            return;

        foreach(ChunkConnector connector in _availableConnectors) 
            CreateRoom(connector, _roomCollection.Rooms[3].gameObject);
    }

    private ChunkConnector GetAvailableConnector(Room room)
    {
        for (int i = 0; i < room.ChunkConnectors.Count; i++)
        {
            if (!room.ChunkConnectors[i].IsConnected)
            {
                return room.ChunkConnectors[i];
            }
        }
        return null;
    }

    public void RestartGeneration()
    {
        ClearLevel();
        StartGeneration();
    }

    public void ClearLevel() 
    {
        Initialize();
        for (int i = 0; i < _createdRooms.Count; i++)
        {
            Destroy(_createdRooms[i].gameObject);
        }
        _createdRooms.Clear();
        _availableConnectors.Clear();
    }
}