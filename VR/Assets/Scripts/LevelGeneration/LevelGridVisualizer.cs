using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelGenerator))]
public class LevelGridVisualizer : MonoBehaviour
{
    [SerializeField] private LevelGenerator _levelGenerator;

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            for(int x = 0; x < _levelGenerator.Grid.Height; x++)
            {
                for(int z = 0; z < _levelGenerator.Grid.Width; z++) 
                {
                    if (_levelGenerator.Grid.GridCells[x, z].IsOccupied) 
                    { 
                        Gizmos.color = Color.red;
                    }
                    else
                    {
                        Gizmos.color = Color.green;
                    }

                    Gizmos.DrawWireCube(new Vector3(x, 0, z)
                        * _levelGenerator.Grid.CellSize,
                        new Vector3(_levelGenerator.Grid.CellSize, 1, _levelGenerator.Grid.CellSize));
                }
            }
        }
    }
}
