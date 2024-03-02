using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelGenaration
{
    public class RoomSelector
    {
        private RoomCollection _roomDataSet;
        private LevelGrid _levelGrid;
        private RoomHandler _roomHandler;

        public RoomSelector(RoomCollection roomDataSet, LevelGrid levelGrid, RoomHandler roomHandler)
        {
            _roomDataSet = roomDataSet;
            _levelGrid = levelGrid;
            _roomHandler = roomHandler;
        }

        public Room SelectRoom(ChunkConnector connector)
        {
            Room room;
            if (_roomHandler.AvailableConnectors.Count == 1)
                room = GetRandomRoom(RoomType.Connector);
            else
                room = GetRandomRoom();

            if (_levelGrid.CanConnectNewRoom(connector, room))
                return room;

            for(int i = 0; i < _roomDataSet.Rooms.Count; i++)
            {
                if (_levelGrid.CanConnectNewRoom(connector, _roomDataSet.Rooms[i]))
                    return _roomDataSet.Rooms[i];
            }

            return null;
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