using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
