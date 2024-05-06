using UnityEngine;
using UnityEditor;
using Game.Enemies;

[CustomEditor(typeof(EnemyDeathHandler))]
public class EnemyDeathHandlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EnemyDeathHandler enemyDeathHandler = (EnemyDeathHandler)target;

        if(GUILayout.Button("Kill"))
            enemyDeathHandler.Die();
    }
}
