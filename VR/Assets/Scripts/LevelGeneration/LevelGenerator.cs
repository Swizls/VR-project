using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private LevelGenerationDataSet _levelGenerationDataSet;

    [SerializeField] private ChunkConnector _startRoomConnector;

    //Parameters
    [SerializeField] private int _minRoomCount;
    [SerializeField] private int _maxRoomCount;

    private List<Room> _createdRooms = new List<Room>();

    private List<ChunkConnector> _availableConnectors = new List<ChunkConnector>();
    
    public List<ChunkConnector> AvailableConnectors => _availableConnectors;

    private void OnValidate()
    {
        if(_minRoomCount > _maxRoomCount)
            _minRoomCount = _maxRoomCount - 1;
    }

    private void Start()
    {
        Room createdRoom = CreateRoom(_startRoomConnector, _levelGenerationDataSet.LevelChunks[0]);

        GenerateLevel(createdRoom);
    }

    private void GenerateLevel(Room previousRoom)
    {
        if (!TryGetAvailableChunkConnector(previousRoom, out ChunkConnector chunkConnector))
        {
            FillEmptySpace();
            return;
        }

        GameObject nextRoom;
        if(_createdRooms.Count < _minRoomCount)
        {
            do
            {
                nextRoom = GetRandomRoom();
                if (nextRoom.GetComponent<Room>().RoomType != RoomTypes.EndRoom)
                {
                    break;
                }
            } while (true);
        }
        else
        {
            nextRoom = GetRandomRoom();
        }

        Room newRoom = CreateRoom(chunkConnector, nextRoom);


        if (_availableConnectors.Count == 0)
            return;

        if (_createdRooms.Count < _maxRoomCount)
            GenerateLevel(newRoom);  
    }

    private Room CreateRoom(ChunkConnector connector, GameObject newRoom, bool flagUpdateConnectors = true)
    {
        Room createdRoom = connector.ConnectNewRoom(newRoom);

        _createdRooms.Add(createdRoom);
        if(flagUpdateConnectors)
            UdpateAvailableConnectors();

        return createdRoom;
    }

    public void FillEmptySpace()
    {
        if(_availableConnectors.Count == 0) 
        {
            return;
        }

        foreach(ChunkConnector connector in _availableConnectors) 
        {
            CreateRoom(connector, _levelGenerationDataSet.LevelChunks[3], false);
        }
    }

    private bool TryGetAvailableChunkConnector(Room room, out ChunkConnector chunkConnector)
    {
        for (int i = 0; i < room.ChunkConnectors.Count; i++)
        {
            if (!room.ChunkConnectors[i].IsConnected)
            {
                chunkConnector = room.ChunkConnectors[i];
                return true;
            }
        }
        chunkConnector = null;
        return false;
    }

    private void UdpateAvailableConnectors()
    {
        foreach (Room room in _createdRooms)
        {
            foreach (ChunkConnector connector in room.ChunkConnectors)
            {
                if (!connector.IsConnected && !_availableConnectors.Contains(connector))
                {
                    _availableConnectors.Add(connector);
                }
                else if(_availableConnectors.Contains(connector))
                {
                    _availableConnectors.Remove(connector);
                }
            }
        }
    }

    private GameObject GetRandomRoom()
    {
        return _levelGenerationDataSet.LevelChunks[Random.Range(0, _levelGenerationDataSet.LevelChunks.Count)];
    }

    public void RestartGeneration()
    {
        ClearLevel();
        Room createdRoom = CreateRoom(_startRoomConnector, _levelGenerationDataSet.LevelChunks[0]);
        GenerateLevel(createdRoom);
    }

    public void ClearLevel() 
    {
        for (int i = 0; i < _createdRooms.Count; i++)
        {
            Destroy(_createdRooms[i].gameObject);
        }
        _createdRooms.Clear();
        _availableConnectors.Clear();
    }
}