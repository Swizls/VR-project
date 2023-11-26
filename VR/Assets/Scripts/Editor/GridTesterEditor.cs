using UnityEditor;
using UnityEngine;

namespace LevelGeneration.DebugTools
{
    [CustomEditor(typeof(GridTester))]
    public class GridTesterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GridTester gridTester = (GridTester)target;

            if (GUILayout.Button("Test Occupation"))
                gridTester.Test();
        }
    }
}
