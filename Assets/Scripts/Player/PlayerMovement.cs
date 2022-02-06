using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;

    //MASCARA PARA DISTINGUIR LO QUE ES SUELO
    [SerializeField] private LayerMask jumpableGround;

    //PRIVATE FLOATS
    public static float dirX;

    public bool canMove = true;
    public bool canJump = true;

    //PUBLIC FLOATS
    public float jumpForce = 15.0f;
    public float moveSpeed = 7.0f;

    //MOVEMENT STATE ENUM
    private enum MovementState { idle, running, jumping, fallling }
    private MovementState state = MovementState.idle;

    //AUDIO
    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        UpdateAnimation();
    }

    //CHECK INPUT
    private void CheckInput()
    {
        //X AXIS DIRECTION AND RUNNING ANIMATION
        dirX = Input.GetAxisRaw("Horizontal");
        if (canMove)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        }

        //JUMP
        if (Input.GetButtonDown("Jump"))
        {
            CheckIfCanJump();
        }
    }

    //JUMP
    private void CheckIfCanJump()
    {
        if (IsGrounded() && canJump)
        {
            Jump();
        }
    }

    private void Jump()
    {
        jumpSoundEffect.Play();
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    //UPDATE ANIMATION
    private void UpdateAnimation()
    {
        //Comprueba si esta corriendo
        if (dirX > 0 && canMove)
        {
            state = MovementState.running;
        }
        else if (dirX < 0 && canMove)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        //Comprueba si esta saltando o callendo
        if (rb.velocity.y > 0.1f && !IsGrounded())
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f && !IsGrounded())
        {
            state = MovementState.fallling;
        }

        //Aplica la animación correspondiente
        anim.SetInteger("state", (int)state);

        //Gira el personaje en la dirección correcta
        FlipSprite();
    }

    //FLIP SPRITE
    private void FlipSprite()
    {
        if (dirX > 0)
        {
            sprite.flipX = false;
        }
        else if (dirX < 0)
        {
            sprite.flipX = true;
        }
    }

    //IS GROUNDED
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    //TOGGLE MOVEMENT
    public void CanMoveToggle()
    {
        canMove = !canMove;
    }

    //TOGGLE MOVEMENT
    public void CanJumpToggle()
    {
        canJump = !canJump;
    }

    public float GetDirX()
    {
        return dirX;
    }
}