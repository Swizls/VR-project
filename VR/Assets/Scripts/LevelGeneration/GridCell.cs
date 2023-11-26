using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    private bool _isOccupied = false;

    public bool IsOccupied => _isOccupied;

    public GridCell() { }

    public void SetCellOccupation()
    {
        _isOccupied = true;
    }
}
