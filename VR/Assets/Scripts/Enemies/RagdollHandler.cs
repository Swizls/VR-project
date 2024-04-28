using UnityEngine;
using UnityEngine.AI;
using EnemyAI;
using System.Linq;

public class RagdollHandler : MonoBehaviour, IHitReaction
{
    [SerializeField] private GameObject _boneRoot;

    //Physics
    private Rigidbody _mainRigidbody;
    private Collider _mainCollider;

    //Components
    private Animator _animator;

    //AI
    private NavMeshAgent _agent;
    private FieldOfView _fieldOfView;
    private EnemyBehaviourHandler _enemyBehaviourHandler;

    private void Start()
    {
        _mainCollider = GetComponent<Collider>();
        _mainRigidbody = GetComponent<Rigidbody>();

        _animator = GetComponentInChildren<Animator>();

        _agent = GetComponent<NavMeshAgent>();
        _fieldOfView = GetComponent<FieldOfView>();
        _enemyBehaviourHandler = GetComponent<EnemyBehaviourHandler>();

        DisableRagdoll();
    }

    public void HitReaction(int dagame)
    {
        if (!_mainCollider.enabled)
            return;
        ActivateRagdoll();
    }

    public void DisableRagdoll()
    {
        _mainCollider.enabled = true;

        _animator.enabled = true;

        _agent.enabled = true;
        _fieldOfView.enabled = true;
        _enemyBehaviourHandler.enabled = true;

        Rigidbody[] childrensRigidBody = _boneRoot.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody child in childrensRigidBody) 
        {
            child.isKinematic = true;
        }
        Collider[] childrensCollider = _boneRoot.GetComponentsInChildren<Collider>();
        foreach (Collider child in childrensCollider)
        {
            child.enabled = false;
        }

    }

    public void ActivateRagdoll()
    {
        _mainCollider.enabled = false;

        _animator.enabled = false;

        _agent.enabled = false;
        _fieldOfView.enabled = false;
        _enemyBehaviourHandler.enabled = false;

        _enemyBehaviourHandler.WarningIcon.SetActive(false);

        Rigidbody[] childrensRigidBody = _boneRoot.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody child in childrensRigidBody)
        {
            child.isKinematic = false;
        }
        Collider[] childrensCollider = _boneRoot.GetComponentsInChildren<Collider>();
        foreach (Collider child in childrensCollider)
        {
            child.enabled = true;
        }
    }
}
