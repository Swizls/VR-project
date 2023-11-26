using UnityEngine;

public class GridTester : MonoBehaviour
{
    [SerializeField] private Room _room;
    [SerializeField] private LevelGenerator _levelGenerator;

    public void Test()
    {
        _levelGenerator.Grid.TrySetOccupationOnGrid(transform.position, _room);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, _room.RoomSize);
    }
}