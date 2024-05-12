using UnityEngine;

public class CustomSnapPoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.2f); ;
        Gizmos.color = Color.red;

        Ray ray = new Ray(transform.position, transform.forward);
        Gizmos.DrawRay(ray);
    }
}
