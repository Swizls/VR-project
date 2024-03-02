using UnityEngine.UIElements;

namespace LevelGenaration
{
    public class GridCell
    {
        private bool _isOccupied = false;
        private bool _isCentralCell = false;
        private Room _roomOnCell = null;

        public bool IsOccupied => _isOccupied;
        public bool IsCentralCell => _isCentralCell;
        public Room RoomOnCell => _roomOnCell;

        public void SetCellOccupation(Room room)
        {
            if (_isOccupied)
                return;

            _isOccupied = true;
            _roomOnCell = room;
        }

        public void SetCellAsCentral()
        {
            _isCentralCell = true;
        }
    }
}
