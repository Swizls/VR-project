using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid
{
    public readonly uint Width;
    public readonly uint Height;
    public readonly uint CellSize;

    private GridCell[,] _gridCells;

    public GridCell[,] GridCells => _gridCells;

    public LevelGrid(uint gridSizeX, uint gridSizeY, uint cellSize)
    {
        Width = gridSizeX;
        Height = gridSizeY;
        CellSize = cellSize;

        _gridCells = new GridCell[Width, Height];

        CreateGrid();
    }

    public bool TrySetOccupationOnGrid(Vector3 position, Room room)
    {
        if (!IsValidPosition(position, room))
            return false;

        Vector2Int cellPosition = ConvertWorldPositionToCellIndex(position);
        Vector2Int roomSize = ConvertRoomSizeToCellSize(room);
        GridCell[,] cellsToOccupy = GetCellsInArea(cellPosition, roomSize);

        foreach (GridCell cell in cellsToOccupy)
        {
            cell.SetCellOccupation();
        }
        return true;
    }

    public bool IsValidPosition(Vector3 position, Room room)
    {
        Vector2Int cellPosition = ConvertWorldPositionToCellIndex(position);
        Vector2Int roomSize = ConvertRoomSizeToCellSize(room);
        GridCell[,] cellsToCheck = GetCellsInArea(cellPosition, roomSize);

        foreach(GridCell cell in cellsToCheck)
        {
            if (cell.IsOccupied)
                return false;
        }
        return true;
    }

    private GridCell[,] GetCellsInArea(Vector2Int position, Vector2Int areaSize)
    {
        GridCell[,] cells = new GridCell[areaSize.x, areaSize.y];

        for (int i = 0; i < areaSize.x; i++)
        {
            for (int j = 0; j < areaSize.y; j++)
            {
                int xIndex = position.x + i;
                int yIndex = position.y + j;

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

    private Vector2Int ConvertWorldPositionToCellIndex(Vector3 position)
    {
        int x = (int)(position.x / CellSize);
        int y = (int)(position.z / CellSize);
        return new Vector2Int(x, y);
    }

    private Vector2Int ConvertRoomSizeToCellSize(Room room)
    {
        int sizeX = room.RoomSize.x / (int)CellSize;
        int sizeY = room.RoomSize.y / (int)CellSize;

        if (sizeX == 0)
            sizeX = 1;
        if (sizeY == 0)
            sizeY = 1;

        return new Vector2Int(sizeX + 1, sizeY + 1);
    }
}
