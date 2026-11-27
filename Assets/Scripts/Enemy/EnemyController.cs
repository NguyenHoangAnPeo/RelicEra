using UnityEngine;

public class EnemyController : BaseMonoBehaviour
{
    protected enum EnemyState
    {
        Patrol,
        Chase
    }

    [Header("References")]
    [SerializeField] protected Transform _target;
    public Transform Target => _target;

    [SerializeField] protected CharacterMovement _movement;
    public CharacterMovement Movement => _movement;

    [SerializeField] protected Health _health;
    public Health Health => _health;

    [Header("Patrol")]
    [SerializeField, Min(0f)] protected float _patrolDistance = 2f;
    public float PatrolDistance => _patrolDistance;

    [SerializeField, Min(0f)] protected float _patrolSpeed = 1.5f;
    public float PatrolSpeed => _patrolSpeed;

    [Header("Chase")]
    [SerializeField, Min(0f)] protected float _detectionRange = 5f;
    public float DetectionRange => _detectionRange;

    [SerializeField, Min(0f)] protected float _loseTargetRange = 7f;
    public float LoseTargetRange => _loseTargetRange;

    [SerializeField, Min(0f)] protected float _chaseSpeed = 4f;
    public float ChaseSpeed => _chaseSpeed;

    [SerializeField, Min(0f)] protected float _stopDistance = 0.8f;
    public float StopDistance => _stopDistance;

    protected EnemyState _state;
    protected Vector2 _patrolOrigin;
    protected int _patrolDirection = 1;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMovement();
        LoadHealth();
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        _patrolOrigin = transform.position;
        _state = EnemyState.Patrol;
        _patrolDirection = 1;
    }

    protected virtual void Update()
    {
        if (_movement == null)
            return;

        if (_health != null && _health.IsDead)
        {
            _movement.StopHorizontal();
            return;
        }

        UpdateState();
    }

    protected virtual void FixedUpdate()
    {
        if (_movement == null || (_health != null && _health.IsDead))
            return;

        switch (_state)
        {
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Chase:
                ChaseTarget();
                break;
        }
    }

    protected virtual void UpdateState()
    {
        if (_target == null)
        {
            _state = EnemyState.Patrol;
            return;
        }

        float distance = Vector2.Distance(transform.position, _target.position);

        if (_state == EnemyState.Patrol && distance <= _detectionRange)
            _state = EnemyState.Chase;
        else if (_state == EnemyState.Chase && distance >= _loseTargetRange)
            _state = EnemyState.Patrol;
    }

    protected virtual void Patrol()
    {
        float leftBound = _patrolOrigin.x - _patrolDistance;
        float rightBound = _patrolOrigin.x + _patrolDistance;

        if (_patrolDirection > 0 && transform.position.x >= rightBound)
            _patrolDirection = -1;
        else if (_patrolDirection < 0 && transform.position.x <= leftBound)
            _patrolDirection = 1;

        _movement.Move(Vector2.right * _patrolDirection, _patrolSpeed);
    }

    protected virtual void ChaseTarget()
    {
        if (_target == null)
        {
            _movement.StopHorizontal();
            return;
        }

        float horizontalDistance = _target.position.x - transform.position.x;
        if (Mathf.Abs(horizontalDistance) <= _stopDistance)
        {
            _movement.StopHorizontal();
            return;
        }

        _movement.Move(Vector2.right * Mathf.Sign(horizontalDistance), _chaseSpeed);
    }

    protected virtual void LoadMovement()
    {
        if (_movement != null)
            return;

        _movement = GetComponentInChildren<CharacterMovement>(true);
    }

    protected virtual void LoadHealth()
    {
        if (_health != null)
            return;

        _health = GetComponentInChildren<Health>(true);
    }
}
