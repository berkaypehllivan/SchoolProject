using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]

public class Goblin : MonoBehaviour
{
    TouchingDirections touchingDirections;
    Animator animator;
    Damageable damageable;

    private CinemachineImpulseSource impulseSource;
    public float walkSpeed = 3f;
    public float walkStopRate = 0.05f;
    private bool isFacingRight = true;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;

    Rigidbody2D rb;

    public enum WalkableDirection { Right, Left }

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                transform.localRotation = Quaternion.Euler(transform.localRotation.x, (transform.localRotation.y == 0f) ? 180f : 0f, transform.localRotation.z);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }


            _walkDirection = value;
        }
    }

    public bool _hasTarget = false;

    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        private set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }
    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;

        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && IsFacingWall())
        {
            FlipDirection();
        }

        if (!damageable.LockVelocity)
        {
            if (CanMove)
                rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
            else
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }
    }

    private bool IsFacingWall()
    {
        return (isFacingRight && touchingDirections.IsOnWall && touchingDirections.wallCheckDirection == Vector2.right) ||
               (!isFacingRight && touchingDirections.IsOnWall && touchingDirections.wallCheckDirection == Vector2.left);
    }

    private void FlipDirection()
    {
        isFacingRight = !isFacingRight;

        WalkDirection = (isFacingRight) ? WalkableDirection.Right : WalkableDirection.Left;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        CameraShakeManager.instance.CameraShake(impulseSource);
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);

        if (damageable != null && damageable.Health <= 0)
        {
            UpdateScoreOnDeath();
        }
    }

    // Skoru güncelleme fonksiyonu
    public void UpdateScoreOnDeath()
    {
        ScoreScript.scoreValue -= 1;
        FindObjectOfType<ScoreScript>().OnEnemyDeath();
        FindObjectOfType<GameManager>().CheckLevelCompletion();
    }

    public void OnCliffDetected()
    {
        if (touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }
}
