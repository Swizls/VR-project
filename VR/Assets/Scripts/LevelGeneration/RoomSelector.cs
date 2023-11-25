using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelGenaration
{
    public class RoomSelector
    {
        private const float DISTANCE_TO_CHECK_OBSTRUCTION = 20f;
        private LevelGenerator _levelGeneratorReference;
        private RoomCollection _roomDataSet;

        public RoomSelector(LevelGenerator levelGeneratorReference, RoomCollection roomsDataSet)
        {
            _levelGeneratorReference = levelGeneratorReference;
            _roomDataSet = roomsDataSet;
        }

        public GameObject SelectRoom(ChunkConnector roomConnector)
        {
            Room nextRoom;
            if (_levelGeneratorReference.AvailableConnectors.Count < _levelGeneratorReference.MinRoomCount)
                nextRoom = GetRandomRoom(RoomType.Connector);
            else
                nextRoom = GetRandomRoom();

            if (IsObsturcionForward(roomConnector.transform))
                nextRoom = _roomDataSet.Rooms[3];

            return nextRoom.gameObject;
        }

        private bool IsObsturcionForward(Transform connectorTranform)
        {
            return Physics.Raycast(connectorTranform.position, -connectorTranform.forward, DISTANCE_TO_CHECK_OBSTRUCTION);
        }

        private Room GetRandomRoom()
        {
            return _roomDataSet.Rooms[Random.Range(0, _roomDataSet.Rooms.Count)];
        }

        private Room GetRandomRoom(RoomType roomType)
        {
            List<Room> roomList = _roomDataSet.Rooms.Where(room => room.RoomType == roomType).ToList();
            return roomList[Random.Range(0, roomList.Count)];
        }
    }
}