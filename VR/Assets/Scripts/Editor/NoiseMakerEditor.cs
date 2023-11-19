using EnemyAI;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NoiseMakerThrowable))]
public class NoiseMakerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        NoiseMaker noiseMaker = (NoiseMaker)target;

        if (GUILayout.Button("Make Noise") && Application.isPlaying)
            noiseMaker.MakeNoise();
    }

    private void OnSceneGUI()
    {
        NoiseMaker noiseMaker = (NoiseMaker)target;
        Handles.color = Color.blue;
        Handles.DrawWireArc(noiseMaker.transform.position + Vector3.up, Vector3.up, Vector3.forward, 360, noiseMaker.NoiseRadius);
    }
}
