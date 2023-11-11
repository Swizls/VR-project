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
    private Rigidbody _mainRigidbody;
    private NavMeshAgent _agent;

    private void Start()
    {
        _mainCollider = GetComponent<Collider>();
        _mainRigidbody = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
    }

    public void HitReaction()
    {
        if (!_mainCollider.enabled)
            return;
        ActivateRagdoll();
    }

    public void ActivateRagdoll()
    {
        _mainCollider.enabled = false;
        _mainRigidbody.isKinematic = true;
        _agent.enabled = false;

        _boneRoot.SetActive(true);
    }
}
