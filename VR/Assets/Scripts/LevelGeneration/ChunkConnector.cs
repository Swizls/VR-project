using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkConnector : MonoBehaviour
{
    private bool _isConnected = false;

    public bool IsConnected => _isConnected;

    public Room ConnectNewRoom(GameObject newRoom)
    {
        GameObject room = Instantiate(newRoom);

        Room roomComponent = room.GetComponent<Room>();

        roomComponent.StartConnector.position = transform.position;
        roomComponent.StartConnector.rotation = transform.rotation;
        _isConnected = true;
        return roomComponent;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -transform.forward);
    }
}