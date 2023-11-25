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

    public List<Room> CreatedRooms => _createdRooms;

    private void OnValidate()
    {
        if(_maxRoomCount < _minRoomCount)
            _maxRoomCount = _minRoomCount - 1;
    }

    private void Start()
    {
        Room firstCreatedRoom = CreateRoom(_startRoomConnector, _levelGenerationDataSet.LevelChunks[0]);

        GenerateLevel(firstCreatedRoom);
    }

    private void GenerateLevel(Room previousRoom)
    {
        ChunkConnector chunkConnector;
        if(TryGetAvailableChunkConnector(previousRoom, out ChunkConnector availabeChunkConnector))
            chunkConnector = availabeChunkConnector;
        else
            chunkConnector = FindAvailableConnector();

        if (chunkConnector == null)
            return;

        GameObject nextRoom = GetRandomRoom();
        //if(_availableConnectors.Count < _minRoomCount)
        //{
        //    do
        //    {
        //        nextRoom = GetRandomRoom();
        //        if (nextRoom.GetComponent<Room>().RoomType != RoomType.EndRoom)
        //        {
        //            break;
        //        }
        //    } while (true);
        //}
        //else
        //{
        //    nextRoom = GetRandomRoom();
        //}
        if (CheckIsEmptySpaceForChunk(chunkConnector.transform))
        {
            do
            {
                nextRoom = GetRandomRoom();
                if (nextRoom.GetComponent<Room>().RoomType == RoomType.EndRoom)
                {
                    break;
                }
            } while (true);
        }

        Room newRoom = CreateRoom(chunkConnector, nextRoom);

        if (_createdRooms.Count < _maxRoomCount)
            GenerateLevel(newRoom);  
        else
            FillEmptySpace();
    }

    private ChunkConnector FindAvailableConnector()
    {
        foreach(Room room in _createdRooms)
        {
            if (room.ChunkConnectors.Count == 0)
                continue;

            foreach(ChunkConnector connector in room.ChunkConnectors)
            {
                if (!connector.IsConnected)
                    return connector;
            }
        }
        return null;
    }

    private bool CheckIsEmptySpaceForChunk(Transform chunkPosition)
    {
        return Physics.Raycast(chunkPosition.position, chunkPosition.transform.forward, 5f);
    }

    private Room CreateRoom(ChunkConnector connector, GameObject newRoom)
    {
        Room createdRoom = connector.ConnectNewRoom(newRoom);

        _createdRooms.Add(createdRoom);

        return createdRoom;
    }

    public void FillEmptySpace()
    {
        if(_availableConnectors.Count == 0) 
            return;

        foreach(ChunkConnector connector in _availableConnectors) 
            CreateRoom(connector, _levelGenerationDataSet.LevelChunks[3]);
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