using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

namespace EnemyAI
{
    [RequireComponent(typeof(FieldOfView))]
    public class EnemyBehaviourHandler : MonoBehaviour
    {
        private enum StartBehaviuor
        {
            Idle,
            Patrol
        }

        [SerializeField] private StartBehaviuor _startBehaviuor;
        [SerializeField] private GameObject _player;
        [SerializeField] private WaypointCointainer _waypointCointainer;

        [Header("Settings")]
        [SerializeField] private Gun _weapon;
        [SerializeField] private int _damage;

        [Header("UI")]
        [SerializeField] private GameObject _warningIcon;

        private Vector3 _positionToSearch;

        public Action<EnemyBehaviour> BehaviourChanged;

        //AI
        private Dictionary<Type, EnemyBehaviour> _enemyBehavioursMap;
        private EnemyBehaviour _currentBehaviour;
        private EnemyMover _enemyMover;
        private FieldOfView _fieldOfView;

        public Gun Weapon => _weapon;
        public EnemyMover EnemyMover => _enemyMover;
        public GameObject WarningIcon => _warningIcon;
        public GameObject PlayerReference => _player;
        public Vector3 PositionToSearch => _positionToSearch;
        public WaypointCointainer WaypointCointainer => _waypointCointainer;

        private void Start()
        {
            _enemyMover = GetComponent<EnemyMover>();
            _fieldOfView = GetComponent<FieldOfView>();

            _waypointCointainer = FindObjectOfType<WaypointCointainer>();

            if(_player == null)
                _player = FindObjectOfType<CharacterController>().gameObject;

            _fieldOfView.PlayerHasBeenSpotted += OnPlayerAppearenceInLineOfSight;
            _fieldOfView.PlayerHasBeenLost += OnPlayerContactLost;

            InitializeBehaviours();
            SetStartBehaviour();
        }

        private void OnEnable()
        {
            if (_fieldOfView == null)
                return;

            _fieldOfView.PlayerHasBeenSpotted += OnPlayerAppearenceInLineOfSight;
            _fieldOfView.PlayerHasBeenLost += OnPlayerContactLost;
        }

        private void OnDisable()
        {
            if (_fieldOfView == null)
                return;

            _fieldOfView.PlayerHasBeenSpotted -= OnPlayerAppearenceInLineOfSight;
            _fieldOfView.PlayerHasBeenLost -= OnPlayerContactLost;
        }

        private void FixedUpdate()
        {
            if(_currentBehaviour.CanBeUpdated)
                _currentBehaviour.Update();
        }

        private void InitializeBehaviours()
        {
            _enemyBehavioursMap = new Dictionary<Type, EnemyBehaviour>();

            _enemyBehavioursMap[typeof(AttackBehaviour)] = new AttackBehaviour(this);
            _enemyBehavioursMap[typeof(ChasePlayerBehaviour)] = new ChasePlayerBehaviour(this);
            _enemyBehavioursMap[typeof(SearchBehaviour)] = new SearchBehaviour(this);
            _enemyBehavioursMap[typeof(IdleBehaviour)] = new IdleBehaviour(this, transform);

            if(_enemyMover.StartWaypoint != null)
                _enemyBehavioursMap[typeof(PatrolBehaviour)] = new PatrolBehaviour(this);
        }

        private void SetBehaviour(EnemyBehaviour newBehaviour)
        {
            if (_currentBehaviour != null)
                _currentBehaviour.Exit();

            _currentBehaviour = newBehaviour;
            _currentBehaviour.Enter();

            BehaviourChanged.Invoke(_currentBehaviour);
        }

        private void SetStartBehaviour()
        {
            EnemyBehaviour startBehaviour;

            switch (_startBehaviuor)
            {
                case StartBehaviuor.Patrol:
                    startBehaviour = GetBehaviour<PatrolBehaviour>();
                    break;
                default:
                    startBehaviour = GetBehaviour<IdleBehaviour>();
                    break;
            }

            SetBehaviour(startBehaviour);
        }

        private EnemyBehaviour GetBehaviour<T>() where T : EnemyBehaviour
        {
            return _enemyBehavioursMap[typeof(T)];
        }

        private void OnPlayerAppearenceInLineOfSight()
        {
            EnemyBehaviour behaviour = GetBehaviour<AttackBehaviour>();
            SetBehaviour(behaviour);
        }

        private void OnPlayerContactLost()
        {
            EnemyBehaviour behaviour = GetBehaviour<SearchBehaviour>();
            behaviour.BehaviourEnded += OnSearchEnd;
            SetBehaviour(behaviour);
        }

        private void OnSearchEnd(EnemyBehaviour behaviour)
        {
            behaviour.BehaviourEnded -= OnSearchEnd;
            ReturnCalmBehaviour();
        }

        private void ReturnCalmBehaviour()
        {
            EnemyBehaviour behaviour;

            if (_enemyMover.StartWaypoint != null)
            {
                behaviour = GetBehaviour<PatrolBehaviour>();
                SetBehaviour(behaviour);
            }
            else
            {
                behaviour = GetBehaviour<IdleBehaviour>();
                SetBehaviour(behaviour);
            }
        }

        private void OnNoiseReactionEnd(EnemyBehaviour behaviour)
        {
            behaviour.BehaviourEnded -= OnNoiseReactionEnd;
            ReturnCalmBehaviour();
        }

        public void ReactionOnNoise(Vector3 noisePosition)
        {
            _positionToSearch = noisePosition;
            EnemyBehaviour behaviour = GetBehaviour<SearchBehaviour>();
            behaviour.BehaviourEnded += OnNoiseReactionEnd;
            SetBehaviour(behaviour);
        }
    }
}