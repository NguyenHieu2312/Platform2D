using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float playerInputHrorizontal;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 17f;
    [SerializeField] private int maxJumpTimes = 2;
    [SerializeField] private int jumpTimeCounter;

    [Header("Check")]
    [SerializeField] private Transform groundCheckpos;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(.5f, .5f);
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private Transform wallCheckPos;
    [SerializeField] private Vector2 wallCheckSize = new Vector2(.002f, .5f);
    [SerializeField] LayerMask wallLayer;
    [HideInInspector] private bool isGrounded;

    [Header("Gravity")]
    [SerializeField] private float gravityBase = 4f;
    [SerializeField] private float maxFallSpeed = 15f;
    [SerializeField] private float gravityMultiplier = 2f;

    [Header("Wall sLide/jump")]
    [SerializeField] private float wallSlideSpeed = 3f;
    [HideInInspector] private bool isWallSliding;
    [SerializeField] private float wallJumpDirection;
    [SerializeField] private float wallJumpTime = .5f;
    [SerializeField] private float wallJumpTimeCounter;
    [SerializeField] private bool isWallJumping;
    [SerializeField] private Vector2 wallJumpForce = new Vector2(5f, 17f);




    [HideInInspector] private bool isFacingRight = true;
    [HideInInspector] private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private ParticleSystem smokeFX;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        anim = GetComponent<Animator>();
        //smokeFX = GetComponent<ParticleSystem>();

    }

    void Update()
    {

        GroundCheck();
        Gravity();
        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            rb.velocity = new Vector2(playerInputHrorizontal * moveSpeed, rb.velocity.y);
            Flip();
        }

        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetFloat("magnitude", rb.velocity.magnitude);
    }

    public void Move(InputAction.CallbackContext context)
    {
       
            playerInputHrorizontal = context.ReadValue<Vector2>().x;       
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(jumpTimeCounter > 0)
        {
            if (context.performed)
            {
                // Hold to strong jump
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter--;
                JumpFX();
            }
            else if (context.canceled && rb.velocity.y > 0)
            {
                // Light jump
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                jumpTimeCounter--;
                JumpFX();
            }
        }

        if (context.performed && wallJumpTimeCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpDirection * wallJumpForce.x, wallJumpForce.y);
            wallJumpTimeCounter = 0;
            JumpFX();

            if (transform.localScale.x != wallJumpDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 currentScale = transform.localScale;
                currentScale.x *= -1f;
                transform.localScale = currentScale;
            }

            Invoke(nameof(CancelWallJump), wallJumpTime + 0.1f);
        }
    }

    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckpos.position, groundCheckSize, 0, groundLayer))
        {
            jumpTimeCounter = maxJumpTimes;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private bool WallCheck()
    {
        return Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer);
    }

    private void WallSlide()
    {
        if (!isGrounded & WallCheck() & playerInputHrorizontal != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -wallSlideSpeed));
            smokeFX.Play();
;        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpDirection = -transform.localScale.x;
            wallJumpTimeCounter = wallJumpTime;

            CancelInvoke(nameof(CancelWallJump));

        }

        else if (wallJumpTimeCounter > 0f)
        {
            wallJumpTimeCounter -= Time.deltaTime;
        }
    }

    private void CancelWallJump()
    {
        isWallJumping = false;
    }
    private void Gravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = gravityBase * gravityMultiplier;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = gravityBase;
        }
    }

    private void Flip() //with scale not rotation
    {
        if (isFacingRight && playerInputHrorizontal < 0 || !isFacingRight && playerInputHrorizontal > 0)
        {
           isFacingRight = !isFacingRight;
            Vector3 currentScale = transform.localScale;
            currentScale.x *= -1f;
            transform.localScale = currentScale;

            if (rb.velocity.y == 0)
            {
                smokeFX.Play();
            }
        }
    }

    private void JumpFX()
    {
        anim.SetTrigger("Jump");
        smokeFX.Play();
    }

    #region

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(groundCheckpos.position, groundCheckSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(wallCheckPos.position, wallCheckSize);
    }
    #endregion
}
