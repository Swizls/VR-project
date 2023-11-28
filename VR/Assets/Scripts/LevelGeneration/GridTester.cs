using UnityEngine;
using LevelGenaration;

public class GridTester : MonoBehaviour
{
    [SerializeField] private Room _room;
    [SerializeField] private LevelGenerator _levelGenerator;

    public void Test()
    {
        _levelGenerator.Grid.TrySetOccupationOnGrid(transform.position, _room);

        Vector2Int position = _levelGenerator.Grid.ConvertWorldPositionToCellIndex(transform.position);

        Debug.Log(position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, new Vector3(_room.RoomSize.x, 5f, _room.RoomSize.y));
    }
}