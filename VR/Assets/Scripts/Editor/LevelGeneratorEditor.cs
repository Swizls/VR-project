using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelGeneration.DebugTools
{
    [CustomEditor(typeof(LevelGenerator))]
    public class LevelGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelGenerator levelGenerator = (LevelGenerator)target;

            if (GUILayout.Button("Restart generation"))
                levelGenerator.RestartGeneration();
            if (GUILayout.Button("Clear level"))
                levelGenerator.ClearLevel();
            if (GUILayout.Button("Available connectors"))
                Debug.Log("Available connectors count: " + levelGenerator.AvailableConnectors.Count);
        }
    }
}
