using CustomDebug;
using UnityEditor;
using UnityEngine;

namespace CustomDebug
{
    [CustomEditor(typeof(DamageDealer))]
    public class DamageDealerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DamageDealer damageDealer = (DamageDealer)target;

            if (GUILayout.Button("Give damage"))
                damageDealer.GiveDamage();
        }
    }
}
