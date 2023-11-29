using UnityEngine;
using LevelGenaration;

public class GridTester : MonoBehaviour
{
    [SerializeField] private Room _room;
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private ConnectorDirectionAxis _direction;

    public void Test()
    {
        _levelGenerator.Grid.TrySetOccupationOnGrid(transform.position, _room, _direction);
    }

    public void GetCellIndex()
    {
        Vector2Int position = _levelGenerator.Grid.ConvertWorldPositionToCellIndex(transform.position);

        Debug.Log(position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 roomRotation = new Vector3(_room.RoomSize.x, 5f, _room.RoomSize.y);
        switch (_direction)
        {
            case ConnectorDirectionAxis.Horizontal:
                roomRotation.Set(roomRotation.z, roomRotation.y, roomRotation.x);
                break;
        }

        Gizmos.DrawCube(transform.position, roomRotation);
    }
}