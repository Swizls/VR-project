using UnityEngine;

namespace Game.Enemies.AI
{ 
    public class WaypointCointainer : MonoBehaviour
    {
        private Waypoint[] _waypoints;

        public Waypoint[] Waypoints => _waypoints;

        private void Start()
        {
            _waypoints = FindObjectsOfType<Waypoint>();
        }
    }
}
