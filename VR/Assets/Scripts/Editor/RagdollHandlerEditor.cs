using UnityEngine;
using UnityEditor;
using Game.Enemies;

[CustomEditor(typeof(RagdollHandler))]
public class RagdollHandlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RagdollHandler ragdollHandler = (RagdollHandler)target;

        if(GUILayout.Button("Activate Ragdoll"))
            ragdollHandler.ActivateRagdoll();
    }
}
