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

    public LevelGrid(uint gridSizeX, uint gridSizeZ, uint cellSize)
    {
        Width = gridSizeX;
        Height = gridSizeZ;
        CellSize = cellSize;

        _gridCells = new GridCell[Width, Height];

        CreateGrid();
    }

    public bool TrySetOccupationOnGrid(Vector3 position, Room room)
    {
        if (!IsValidPosition(position, room))
            return false;

        ConvertWorldPositionToCellIndex(position, out int x, out int y);
        for (int height = y - (int)room.RoomSize.z / 2; height < y + (int)room.RoomSize.z / 2; height++)
        {
            for (int width = x - (int)room.RoomSize.x / 2; width < x + (int)room.RoomSize.x / 2; width++)
            {
                _gridCells[width, height].SetCellOccupation();
            }
        }
        return true;
    }

    public bool IsValidPosition(Vector3 position, Room room)
    {
        ConvertWorldPositionToCellIndex(position, out int x, out int y);

        for (int height = y - (int)room.RoomSize.z/2; height < y + (int)room.RoomSize.z/2; height++)
        {
            for (int width = x - (int)room.RoomSize.x / 2; width < x + (int)room.RoomSize.x / 2; width++)
            {
                if (_gridCells[width, height].IsOccupied)
                {
                    return false;
                }
            }
        }
        return true;
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

    private void ConvertWorldPositionToCellIndex(Vector3 position, out int x, out int y)
    {
        x = (int)(position.x / CellSize);
        y = (int)(position.z / CellSize);
    }
}
