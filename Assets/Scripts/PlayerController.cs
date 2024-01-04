using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]

public class PlayerController : MonoBehaviour

{
    private CinemachineImpulseSource impulseSource;
    FadeInOut fade;

    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 5f;
    private float vertical;
    public float wallSlideSpeed = 5f;
    public float wallClimbSpeed = 5f;

    [Header("JumpSystem")]
    private float jumpImpulse = 17f;
    private bool doubleJump;
    private float doubleJumpingPower = 13f;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    [SerializeField] private float fallMultiplier;


    [SerializeField] private AudioSource jumpClip;

    Vector2 vecGravity;
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    TouchingDirections touchingDirections;
    Damageable damageable;

    public float CurrentMoveSpeed { get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.PlayerIsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        //Air Move
                        return airWalkSpeed;
                    }
                }
                else
                {
                    //Idle Speed is 0
                    return 0;
                }
            } else
            {
                //Movement Locked
                return 0;
            }
        }
                
        }

    private bool _isMoving = false;

    public bool IsMoving { get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool IsFacingRight;

    public bool CanMove {  get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    private void Awake()
    {
        fade = FindAnyObjectByType<FadeInOut>();

        impulseSource = GetComponent<CinemachineImpulseSource>();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
    }

    private void FixedUpdate()
    {
        if (!damageable.LockVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

        // fallMultiplier(Hýzlý Düþme) Entegrasyonu
        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }

        //CoyoteTime(Yerden ayrýldýktan 0.2 saniye sonra zýplama entegrasyonu)
        if (touchingDirections.IsGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (touchingDirections.PlayerIsOnWall && !touchingDirections.IsGrounded)
        {
            HandleWallSlide();
            HandleWallClimb();
        }
    }

    void HandleWallSlide()
    {
        // Duvar kaymasý
        if (rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }
    }

    void HandleWallClimb()
    {
        vertical = Input.GetAxisRaw("Vertical");

        if (touchingDirections.PlayerIsOnWall && Mathf.Abs(vertical) > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, vertical * wallClimbSpeed);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        } else
        {
            IsMoving = false;
        }

    }

    private void Turn()
    {
        if (IsFacingRight)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            Turn();
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            Turn();
        }

    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void onJump(InputAction.CallbackContext context)
    {

        if (touchingDirections.IsGrounded && context.started)
        {
            doubleJump = false;

            jumpClip.Play();
        }

        if(context.started)
            {

            if (coyoteTimeCounter > 0f || doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, doubleJump ? doubleJumpingPower : jumpImpulse);
                jumpClip.Play();
                doubleJump = !doubleJump;
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }
    }

    public void onAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        CameraShakeManager.instance.CameraShake(impulseSource);
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);

        if (!IsAlive)
        {
            // Öldüðümüzde kaydedilen level bilgisini güncelle
            UpdateSavedLevel();
            StartCoroutine(FadeOutAndLoadScene());
            CameraManager.instance.StopFollowingPlayer();
        }
    }

    private void UpdateSavedLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedLevel", currentLevel);
    }

    private IEnumerator FadeOutAndLoadScene()
    {
        yield return new WaitForSeconds(1f);
        fade.FadeIn();
        yield return new WaitForSeconds(1f);
        fade.FadeOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
}
