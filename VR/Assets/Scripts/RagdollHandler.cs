using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    [SerializeField] private GameObject _boneRoot;

    private Collider _mainCollider;

    private void Start()
    {
        _mainCollider = GetComponent<Collider>();

        ToggleRagdoll();
    }

    public void ToggleRagdoll()
    {
        foreach(Collider bone in _boneRoot.GetComponentsInChildren<Collider>())
        {
            bone.enabled = !bone.enabled;
        }
        foreach(Rigidbody bone in _boneRoot.GetComponentsInChildren<Rigidbody>())
        {
            bone.isKinematic = !bone.isKinematic;
        }
    }
}
