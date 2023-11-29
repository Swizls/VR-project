using System;
using UnityEngine;

namespace LevelGenaration
{
    public class LevelGrid
    {
        public readonly int Width;
        public readonly int Height;
        public readonly int CellSize;

        private GridCell[,] _gridCells;

        public GridCell[,] GridCells => _gridCells;

        public LevelGrid(int gridSizeX, int gridSizeY, int cellSize)
        {
            if (gridSizeX < 1 || gridSizeY < 1 || cellSize < 1)
                throw new System.ArgumentException("Wrong grid values");

            Width = gridSizeX;
            Height = gridSizeY;
            CellSize = cellSize;

            _gridCells = new GridCell[Width, Height];

            CreateGrid();
        }

        public bool TrySetOccupationOnGrid(Vector3 position, Room room, ConnectorDirectionAxis directionAxis = ConnectorDirectionAxis.Vertical)
        {
            Vector2Int roomSize = ConvertRoomSizeToCellSize(room);
            if (directionAxis != ConnectorDirectionAxis.Vertical)
                roomSize.Set(roomSize.y, roomSize.x);

            GridCell[,] cellsToOccupy = GetCellsInArea(position, roomSize);

            if (!IsValidPosition(cellsToOccupy))
                return false;

            foreach (GridCell cell in cellsToOccupy)
            {
                cell.SetCellOccupation(room);
            }
            return true;
        }

        public bool IsValidPosition(GridCell[,] cellsToCheck)
        {
            foreach(GridCell cell in cellsToCheck)
            {
                if (cell.IsOccupied)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsValidPosition(ChunkConnector connector, Room room)
        {
            Vector2Int roomSize = ConvertRoomSizeToCellSize(room);
            if (connector.Direction != ConnectorDirectionAxis.Vertical)
                roomSize.Set(roomSize.y, roomSize.x);

            GridCell[,] cellsToCheck = GetCellsInArea(connector.transform.position, roomSize);

            foreach (GridCell cell in cellsToCheck)
            {
                if (cell.IsOccupied)
                {
                    if (cell.RoomOnCell != connector.ConnectorsRoom)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public Vector3Int GetRandomPositionOnGrid()
        {
            int randomWidth = UnityEngine.Random.Range(20, Width - 20) * CellSize;
            int randomHeight = UnityEngine.Random.Range(20, Height - 20) * CellSize;
            return new Vector3Int(randomWidth, 0, randomHeight);
        }

        private GridCell[,] GetCellsInArea(Vector3 position, Vector2Int areaSize, ConnectorDirectionAxis directionAxis = ConnectorDirectionAxis.Vertical)
        {
            if(directionAxis != ConnectorDirectionAxis.Vertical) 
            {
                areaSize = new Vector2Int(areaSize.y, areaSize.x);
            }

            Vector2Int centralCellIndex = ConvertWorldPositionToCellIndex(position);
            _gridCells[centralCellIndex.x, centralCellIndex.y].SetCellAsCentral();
            Vector3 cellWorldPosition = ConvertCellPositionToWorld(centralCellIndex);

            Vector3 direction = (position - cellWorldPosition).normalized;

            Vector2Int actualAreaSize = new Vector2Int(areaSize.x + Mathf.Abs(Mathf.RoundToInt(direction.x)),
                areaSize.y + Mathf.Abs(Mathf.RoundToInt(direction.z)));
            GridCell[,] cells = new GridCell[actualAreaSize.x, actualAreaSize.y];

            for (int i = 0; i < actualAreaSize.x; i++)
            {
                for (int j = 0; j < actualAreaSize.y; j++)
                {
                    int xIndex = centralCellIndex.x + (i - actualAreaSize.x / 2);
                    int yIndex = centralCellIndex.y + (j - actualAreaSize.y / 2);

                    if (xIndex >= 0 && xIndex < _gridCells.GetLength(0) &&
                        yIndex >= 0 && yIndex < _gridCells.GetLength(1))
                    {
                        cells[i, j] = _gridCells[xIndex, yIndex];
                    }
                    else
                    {
                        cells[i, j] = null;
                    }
                }
            }

            return cells;
        }

        private void CreateGrid()
        {
            for(int x = 0; x < Height; x++) 
            { 
                for(int y = 0; y < Width; y++) 
                {
                    _gridCells[x, y] = new GridCell();
                }
            }
        }

        public Vector2Int ConvertWorldPositionToCellIndex(Vector3 position)
        {
            int x = Mathf.RoundToInt(position.x / CellSize);
            int y = Mathf.RoundToInt(position.z / CellSize);
            return new Vector2Int(x, y);
        }

        public Vector3 ConvertCellPositionToWorld(Vector2Int position)
        {
            int x = position.x * CellSize;
            int z = position.y * CellSize;
            return new Vector3(x, 0, z);
        }

        private Vector2Int ConvertRoomSizeToCellSize(Room room)
        {
            float sizeX = room.RoomSize.x / CellSize;
            float sizeY = room.RoomSize.y / CellSize;

            return new Vector2Int(Mathf.CeilToInt(sizeX), Mathf.CeilToInt(sizeY));
        }
    }
}
