using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkConnector : MonoBehaviour
{
    private bool _isConnected = false;

    public bool IsConnected => _isConnected;

    public Room ConnectNewRoom(Room newRoom)
    {
        Instantiate(newRoom.gameObject);
        newRoom.StartConnector.position = transform.position;
        _isConnected = true;
        return newRoom;
    }
}
