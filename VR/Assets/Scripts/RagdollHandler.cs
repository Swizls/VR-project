using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        ActivateRagdoll();
    }

    public void HitReaction()
    {
        if (!_mainCollider.enabled)
            return;

        _agent.enabled = false;
        ActivateRagdoll();
    }

    public void ActivateRagdoll()
    {
        foreach(Collider bone in _boneRoot.GetComponentsInChildren<Collider>())
        {
            bone.enabled = true;
        }
        foreach(Rigidbody bone in _boneRoot.GetComponentsInChildren<Rigidbody>())
        {
            bone.isKinematic = true;
        }
    }
}
