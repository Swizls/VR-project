using UnityEditor;
using UnityEngine;
using LevelGenaration;

namespace LevelGeneration.DebugTools
{
    [CustomEditor(typeof(Room))]
    public class RoomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (Application.isPlaying)
                return;

            Room room = (Room)target;

            if (GUILayout.Button("Calculate room size"))
                room.CalculateRoomSizeAndCenter();           
        }
    }
}
