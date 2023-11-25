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

            if (!Application.isPlaying)
            {
                return;
            }

            if (GUILayout.Button("Restart generation"))
            {
                levelGenerator.RestartGeneration();
            }
            if (GUILayout.Button("Clear level"))
            {
                levelGenerator.ClearLevel();
            }
        }
    }
}
