using UnityEngine;
using UnityEditor;
using EnemyAI;

[CustomEditor(typeof(EnemyBehaviourHandler))]
public class BehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EnemyBehaviourHandler enemyBehaviourHandler = (EnemyBehaviourHandler)target;

        GUILayout.TextField("Current behaviour: " + enemyBehaviourHandler.CurrentBehaviour);
    }
}
