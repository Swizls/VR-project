using UnityEngine;

namespace Game.Enemies.AI
{ 
    public class Waypoint : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawSphere(transform.position, 0.1f);
        }
    }
}

