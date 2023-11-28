namespace LevelGenaration
{
    public class GridCell
    {
        private bool _isOccupied = false;
        private Room _roomOnCell = null;

        public bool IsOccupied => _isOccupied;
        public Room RoomOnCell => _roomOnCell;

        public void SetCellOccupation(Room room)
        {
            _isOccupied = true;
            _roomOnCell = room;
        }
    }
}
