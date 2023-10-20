using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;

public class RagdollHandler : MonoBehaviour, HitReaction
{
    [SerializeField] private GameObject _boneRoot;

    private Collider _mainCollider;
    private NavMeshAgent _agent;

    private void Start()
    {
        _mainCollider = GetComponent<Collider>();
        _agent = GetComponent<NavMeshAgent>();

        ToggleRagdoll();
    }

    public void HitReaction()
    {
        ToggleRagdoll();
        _agent.enabled = !_agent.enabled;
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
