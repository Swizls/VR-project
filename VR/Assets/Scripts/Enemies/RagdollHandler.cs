using UnityEngine;
using UnityEngine.AI;
using EnemyAI;

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
    }

    public void HitReaction(int dagame)
    {
        if (!_mainCollider.enabled)
            return;
        ActivateRagdoll();
    }

    public void ActivateRagdoll()
    {
        _mainRigidbody.isKinematic = true;
        _mainCollider.enabled = false;

        _animator.enabled = false;

        _agent.enabled = false;
        _fieldOfView.enabled = false;
        _enemyBehaviourHandler.enabled = false;

        _enemyBehaviourHandler.WarningIcon.SetActive(false);

        _boneRoot.SetActive(true);
    }
}
