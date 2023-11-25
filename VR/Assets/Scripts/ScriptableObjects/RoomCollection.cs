using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default level generation data set", menuName = "Generation data sets/Default level generation data set")]
public class RoomCollection : ScriptableObject
{
    [SerializeField] private List<Room> _rooms;

    public List<Room> Rooms => _rooms;
}
