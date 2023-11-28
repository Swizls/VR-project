using UnityEditor;
using UnityEngine;
using LevelGenaration;

namespace LevelGeneration.DebugTools 
{
    [CustomEditor(typeof(ChunkConnector))]
    public class ChunkConnectorEditor : Editor 
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ChunkConnector chunkConnector = (ChunkConnector)target;

            if (GUILayout.Button("Check connection"))
                Debug.Log("Chunk connection: " + chunkConnector.IsConnected);
        }
    }
}