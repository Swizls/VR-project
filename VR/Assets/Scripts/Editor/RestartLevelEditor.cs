using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RestartLevel))]
public class RestartLevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RestartLevel restartHanlder = (RestartLevel)target;

        if(Application.isPlaying)
        {
            if (GUILayout.Button("Restart Level"))
            {
                restartHanlder.Restart();
            }
        }
    }
}