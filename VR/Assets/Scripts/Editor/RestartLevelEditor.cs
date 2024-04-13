using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneHandler))]
public class RestartLevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SceneHandler restartHanlder = (SceneHandler)target;

        if(Application.isPlaying)
        {
            if (GUILayout.Button("Restart Level"))
            {
                restartHanlder.Restart();
            }
        }
    }
}