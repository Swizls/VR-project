using System;
using UnityEngine;

namespace LevelGenaration 
{ 
    public class RoomPlacer
    {
        private LevelGrid _grid;

        public Action<Room> RoomPlaced;

        public RoomPlacer(LevelGrid grid)
        {
            _grid = grid;
        }

        public void PlaceRoom(ChunkConnector connector, Room room)
        {
            Room createdRoom = connector.ConnectNewRoom(room.gameObject);
            _grid.SetOccupationOnGrid(connector.transform.position, createdRoom, connector.Direction);
            RoomPlaced?.Invoke(createdRoom);
        }
    }
}
