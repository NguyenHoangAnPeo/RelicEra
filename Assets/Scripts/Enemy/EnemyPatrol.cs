using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected CharacterMovement _movement;
    [SerializeField] protected Collider2D _collider2D;

    [Header("Ground Detection")]
    [SerializeField] protected LayerMask _groundLayer;
    [SerializeField, Min(0.01f)] protected float _groundCheckDistance = 0.2f;
    [SerializeField, Min(0f)] protected float _edgeOffset = 0.05f;
    [SerializeField, Min(0.01f)] protected float _obstacleCheckDistance = 0.1f;

    protected int _direction = 1;

    protected virtual void Awake()
    {
        LoadComponents();

        if (_movement != null && !_movement.IsFacingRight)
            _direction = -1;
    }

    public virtual void Patrol(float speed)
    {
        if (_movement == null || _collider2D == null)
            return;

        if (IsEdgeAhead() || IsObstacleAhead())
            _direction *= -1;

        _movement.Move(Vector2.right * _direction, speed);
    }

    protected virtual bool IsEdgeAhead()
    {
        Bounds bounds = _collider2D.bounds;
        float probeX = _direction > 0
            ? bounds.max.x + _edgeOffset
            : bounds.min.x - _edgeOffset;
        Vector2 origin = new Vector2(probeX, bounds.min.y + _edgeOffset);

        return !Physics2D.Raycast(origin, Vector2.down, _groundCheckDistance, _groundLayer);
    }

    protected virtual bool IsObstacleAhead()
    {
        Bounds bounds = _collider2D.bounds;
        float probeX = _direction > 0
            ? bounds.max.x + _edgeOffset
            : bounds.min.x - _edgeOffset;
        Vector2 origin = new Vector2(probeX, bounds.center.y);

        return Physics2D.Raycast(origin, Vector2.right * _direction, _obstacleCheckDistance, _groundLayer);
    }

    protected virtual void LoadComponents()
    {
        if (_movement == null)
            _movement = GetComponentInChildren<CharacterMovement>(true);

        if (_collider2D == null)
            _collider2D = GetComponentInChildren<Collider2D>(true);
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Collider2D patrolCollider = _collider2D != null
            ? _collider2D
            : GetComponentInChildren<Collider2D>(true);

        if (patrolCollider == null)
            return;

        Bounds bounds = patrolCollider.bounds;
        float direction = _direction == 0 ? 1f : _direction;
        float probeX = direction > 0
            ? bounds.max.x + _edgeOffset
            : bounds.min.x - _edgeOffset;
        Vector2 groundOrigin = new Vector2(probeX, bounds.min.y + _edgeOffset);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(groundOrigin, groundOrigin + Vector2.down * _groundCheckDistance);
        Gizmos.color = Color.red;
        Vector2 obstacleOrigin = new Vector2(
            direction > 0 ? bounds.max.x + _edgeOffset : bounds.min.x - _edgeOffset,
            bounds.center.y);
        Gizmos.DrawLine(obstacleOrigin,
            obstacleOrigin + Vector2.right * direction * _obstacleCheckDistance);
    }
}
