using LevelGenaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private RoomCollection _roomCollection;

    [SerializeField] private int _minRoomCount;
    [SerializeField] private int _maxRoomCount;

    [SerializeField] private bool _generateLevelOnStart;

    [Space]
    [Header("Grid settings")]

    [SerializeField] private int _gridHeight;
    [SerializeField] private int _gridWidth;
    [SerializeField] private int _cellSize;

    private LevelGrid _grid;
    private RoomHandler _roomHandler;
    private RoomSelector _roomSelector;
    private RoomPlacer _roomPlacer;

    private List<Room> _createdRooms = new List<Room>();

    public LevelGrid Grid => _grid;

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
        _roomHandler.StartRoom = startRoom;
        _grid.SetOccupationOnGrid(startRoom.transform.position, startRoom);
        startRoom.transform.position += _roomCollection.StartRoom.RoomOffsetFromPivot;

        StartCoroutine(GenerateLevel(startRoom));
    }

    private void Initialize()
    {
        _grid = new LevelGrid(_gridWidth, _gridWidth, _cellSize);
        _roomPlacer = new RoomPlacer(_grid);
        _roomHandler = new RoomHandler(_roomPlacer);
        _roomSelector = new RoomSelector(_roomCollection, _grid, _roomHandler);
    }

    private IEnumerator GenerateLevel(Room previousRoom)
    {
        while (_roomHandler.CreatedRoom.Count < _maxRoomCount || _roomHandler.AvailableConnectors.Count > 0)
        {
            yield return new WaitForSeconds(1);
            ChunkConnector connector = _roomHandler.GetAvailableConnector();

            Room selectedRoom = _roomSelector.SelectRoom(connector);

            if (selectedRoom != null)
                _roomPlacer.PlaceRoom(connector, selectedRoom);
            else
                throw new System.Exception("New room is null");
        }
    }
    
    public void RestartGeneration()
    {
        ClearLevel();
        Initialize();
        StartGeneration();
    }
    private void ClearLevel()
    {
        if (_roomHandler.CreatedRoom.Count == 0)
            return;

        foreach (Room room in _roomHandler.CreatedRoom)
        {
            Destroy(room.gameObject);
        }
    }
}