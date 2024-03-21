using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    bool isFacingRight = true;
    public ParticleSystem smokeFX;

    public DoubleJumpUI doubleUI;

    [Header("Movement")]
    public float moveSpeed = 5f;
    float horizontalMovement;
    [Header("Jumping")]
    public float jumpPower = 12f;
    public int maxJumps = 1;
    int jumpsRemaining;
    bool doubleJumpEnabled = false;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallspeed = 18f;
    public float fallSpeedMultiplier = 2f;


    // Like, comment and subscribe to events or whatever :_)
    void Start()
    {
        DoubleJump.OnDoubleJumpCollect += DoubleJumpActive;
        doubleUI.SetDoubleJump(false);
    }

    // Update is called once per frame
    // Handles animation and calls the other functions to handle player schmovement
    void Update()
    {
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        GroundCheck();
        Gravity();
        Flip();

        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetFloat("magnitude", rb.velocity.magnitude);
    }

    // Apply gravity to the player
    private void Gravity()
    {
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallspeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    // Handle horizontal movement input
    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    // Handle jump input
    public void Jump(InputAction.CallbackContext context)
    {
        if(jumpsRemaining > 0)
        {
            if (context.performed)
            {
                // Hold down for full height
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                jumpsRemaining--;
                SoundEffectManager.Play("PlayerJump");
                JumpFX();
            }
            else if (context.canceled)
            {
                // Light tap for half the height
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                jumpsRemaining--;
                JumpFX();
            }
        }
    }

    // Play jump visual effects
    private void JumpFX()
    {
        animator.SetTrigger("jump");
        smokeFX.Play();
    }

    // Check for ground
    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
        }
    }

    // Flips the sprite based on movement direction
    private void Flip()
    {
        if(isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
            if(rb.velocity.y == 0)
            {
                smokeFX.Play();
            }
        }
    }

    // Activate or deactive the double jump ability
    public void DoubleJumpActive(bool isDoubleJumpEnabled)
    {
        doubleJumpEnabled = isDoubleJumpEnabled;
        maxJumps = isDoubleJumpEnabled ? 2 : 1;
        if (doubleJumpEnabled == false)
        {
            doubleUI.SetDoubleJump(false);
        }
        else
        {
            doubleUI.SetDoubleJump(true);
        }
    }

    // This is just to see the box underneath the player clearer
    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }

}