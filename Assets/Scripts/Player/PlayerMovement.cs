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

    private static float contadorStart;
    private static float contador;

    public bool canMove2 = true;
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
    [SerializeField] private AudioSource runSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        contadorStart = 1;
        contador = contadorStart;
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
        contador -= 1* Time.deltaTime;
        if(rb.velocity.x ==0 || !IsGrounded()){
            runSoundEffect.Stop();
        }
    }

    //CHECK INPUT
    private void CheckInput()
    {
        //X AXIS DIRECTION AND RUNNING ANIMATION
        dirX = Input.GetAxisRaw("Horizontal");
        if (canMove && canMove2)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
            if( contador <= 0 && rb.velocity.x !=0){
                runSoundEffect.Play();
                contador = contadorStart;
            }else{
                contador -= Time.deltaTime;
            }
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
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("AttackLvL2"))
        {

            //Comprueba si esta corriendo
            if (dirX > 0 && canMove && canMove2)
            {
                state = MovementState.running;
            }
            else if (dirX < 0 && canMove && canMove2)
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

            //Aplica la animaci�n correspondiente
            anim.SetInteger("state", (int)state);

            //Gira el personaje en la direcci�n correcta
            FlipSprite();

        }
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