using System;
using System.Collections.Generic;
using System.Linq;

namespace LevelGenaration
{
    public class RoomHandler
    {
        private ChunkConnector _currentConnector;
        private List<Room> _createdRooms = new List<Room>();
        private List<ChunkConnector> _availableConnectors = new List<ChunkConnector>();
        private Room _startRoom;

        public ChunkConnector CurrentConnector => _currentConnector;
        public List<Room> CreatedRoom => _createdRooms;
        public List<ChunkConnector> AvailableConnectors => _availableConnectors;
        public Room StartRoom
        {
            get
            {
                return _startRoom;
            }
            set
            {
                if(_startRoom == null)
                {
                    _startRoom = value;
                    AddRoom(_startRoom);
                }
                else 
                {
                    throw new System.InvalidOperationException("Start room is already setted");
                }
            }
        }
        public RoomHandler(RoomPlacer roomPlacer)
        {
            roomPlacer.RoomPlaced += OnRoomPlacment;
        }

        public ChunkConnector GetAvailableConnector()
        {
            if(_currentConnector != null && !_currentConnector.IsConnected)
                return _currentConnector;

            if (_availableConnectors.Count == 0)
            {
                _currentConnector = null;
                return _currentConnector;
            }
            else
            {
                _currentConnector = _availableConnectors[0];
                return _currentConnector;
            }
        }

        private void OnRoomPlacment(Room room)
        {
            AddRoom(room);
        }

        private void AddRoom(Room room)
        {
            if (!_createdRooms.Contains(room))
            {
                _createdRooms.Add(room);
                UpdateAvailableConnectors();
            }
            else
            {
                throw new System.ArgumentException("This room is already contained in created room list");
            }
        }

        private void UpdateAvailableConnectors()
        {
            foreach(Room room in _createdRooms) 
            { 
                foreach(ChunkConnector connector in room.ChunkConnectors)
                {
                    if (connector.IsConnected)
                    {
                        _availableConnectors.Remove(connector);
                    }
                    else
                    {
                        _availableConnectors.Add(connector);
                    }
                }
            }
        }
    }
}
