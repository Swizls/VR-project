using EnemyAI;
using System;

public abstract class EnemyBehaviour
{
    protected EnemyBehaviourHandler _enemyReference;

    protected bool _canBeUpdated;

    public Action<EnemyBehaviour> BehaviourEnded;

    public bool CanBeUpdated => _canBeUpdated;

    public EnemyBehaviour(EnemyBehaviourHandler enemyReference)
    {
        _enemyReference = enemyReference;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}