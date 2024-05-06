using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HolsterPositionHandler))]
public class HolsterPositionEditor : Editor
{
    private void OnSceneGUI()
    {
        HolsterPositionHandler holster = (HolsterPositionHandler)target;

        Handles.color = Color.red;
        Handles.DrawWireArc(holster.CameraReference.position, Vector3.up, Vector3.forward, 360, holster.CameraOffset.x, 2f);
        Handles.DrawWireCube(holster.CalculatePositionForRotation(), new Vector3(0.1f, 0.1f, 0.1f));
    }
}
