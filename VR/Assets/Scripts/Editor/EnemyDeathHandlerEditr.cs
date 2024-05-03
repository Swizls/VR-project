using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyDeathHandler))]
public class EnemyDeathHandlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EnemyDeathHandler ragdollHandler = (EnemyDeathHandler)target;

        if(GUILayout.Button("Kill"))
            ragdollHandler.Die();
    }
}
