using UnityEngine;

namespace LevelGenaration
{ 
    public class ChunkConnector : MonoBehaviour
    {
        private Room _connectorsRoom;
        private bool _isConnected = false;
        private ConnectorDirectionAxis _direction;

        public ConnectorDirectionAxis Direction => _direction;
        public bool IsConnected => _isConnected;
        public Room ConnectorsRoom => _connectorsRoom;

        private void Start()
        {
            if (_connectorsRoom == null)
                _connectorsRoom = GetComponentInParent<Room>();

            SetConnectorDirectionAxis();
        }

        private void SetConnectorDirectionAxis()
        {
            switch (transform.rotation.eulerAngles.y)
            {
                case 90:
                case 270:
                    _direction = ConnectorDirectionAxis.Horizontal;
                    break;
                default:
                    _direction = ConnectorDirectionAxis.Vertical;
                    break;
            }

        }

        public Room ConnectNewRoom(GameObject nextRoom)
        {
            GameObject createdRoom = Instantiate(nextRoom);

            Room createdRoomComponent = createdRoom.GetComponent<Room>();

            createdRoomComponent.SetPreviousRoom(_connectorsRoom);

            createdRoomComponent.StartConnector.position = transform.position;
            createdRoomComponent.StartConnector.rotation = transform.rotation;
            _isConnected = true;
            return createdRoomComponent;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, -transform.forward);
        }
    }
}
