using UnityEngine;
using UnityEngine.AI;
using EnemyAI;

public class RagdollHandler : MonoBehaviour, HitReaction
{
    [SerializeField] private GameObject _boneRoot;

    //Physics
    private Rigidbody _mainRigidbody;
    private Collider _mainCollider;

    //AI
    private NavMeshAgent _agent;
    private FieldOfView _fieldOfView;
    private EnemyBehaviourHandler _enemyBehaviourHandler;

    private void Start()
    {
        _mainCollider = GetComponent<Collider>();
        _mainRigidbody = GetComponent<Rigidbody>();

        _agent = GetComponent<NavMeshAgent>();
        _fieldOfView = GetComponent<FieldOfView>();
        _enemyBehaviourHandler = GetComponent<EnemyBehaviourHandler>();
    }

    public void HitReaction()
    {
        if (!_mainCollider.enabled)
            return;
        ActivateRagdoll();
    }

    public void ActivateRagdoll()
    {
        _mainRigidbody.isKinematic = true;

        _mainCollider.enabled = false;
        _agent.enabled = false;
        _fieldOfView.enabled = false;
        _enemyBehaviourHandler.enabled = false;

        _enemyBehaviourHandler.WarningIcon.SetActive(false);

        _boneRoot.SetActive(true);
    }
}
