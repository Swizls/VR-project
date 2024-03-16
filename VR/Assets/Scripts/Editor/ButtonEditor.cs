using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Button))]
public class ButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Button button = (Button)target;

        if (GUILayout.Button("Press button"))
            button.Press();
    }
}
