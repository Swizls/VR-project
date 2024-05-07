using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RigTester))]
public class RigTesterEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RigTester rig = (RigTester)target;

        if (GUILayout.Button("Toggle Rig"))
            rig.ToggleEditorRig();
    }
}