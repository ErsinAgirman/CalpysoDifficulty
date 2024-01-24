using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask jumpableGround;
    private bool isRunning = false;
    private float dirX = 0f;
    private bool canDoubleJump = false;
    PlayerCombat playerCombat;

    private enum MovementState {idle, walking, jumping, falling, running}
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerCombat=GetComponent<PlayerCombat>();
    }

    void Update()
    {


       if(GetComponent<Health>().DieProperty)
       return;

        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * (isRunning ? runSpeed : speed), rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = true; // Birincil zıplama yapıldığında çift zıplama yapma izni ver
            }
            else if (canDoubleJump) // Havada ve çift zıplama izni varsa
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = false; // Çift zıplama yapıldığında izni kapat
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false; 
        }

        if (Input.GetKeyDown(KeyCode.S) && !IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, -jumpForce/2);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = isRunning ? MovementState.running : MovementState.walking;
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x),transform.localScale.y);
        }

        else if (dirX < 0f)
        {
            state = isRunning ? MovementState.running : MovementState.walking;
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x),transform.localScale.y);
        }

        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }

        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
