using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapizEmbiste : MonoBehaviour
{
    public bool mustPatrol;
    public bool attacking = false;

    public float movementSpeed = 5;
    public int maxSteps = 100;
    public int steps = 0;
    public bool stunned = false;

    private int counter;
    public int dirX = 1;

    public Rigidbody2D rb;
    public Transform transform;
    public BoxCollider2D collider;

    public LayerMask playerLayer;

    public Vector2 vision1;
    public Vector2 vision2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        collider = GetComponent<BoxCollider2D>();
        mustPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        vision1 = new Vector2(rb.position.x, rb.position.y - 2);
        vision2 = vision1 + new Vector2(dirX*7, 0.3f);

        

        if (!stunned)
        {
            if (mustPatrol)
            {
                Patrol();
            }

            Collider2D player = Physics2D.OverlapArea(vision1, vision2, playerLayer);
            if (player == true)
            {
                Attack();
            }
        }

        if (counter > 0)
        {
            counter--;
            rb.velocity = new Vector2(0, 0);
        }
        else if (counter == 0)
        {
            stunned = false;
            counter = -1;
        }

    }

    void Patrol()
    {
        steps++;
        if (steps >= maxSteps)
        {
            Flip();
        }

        rb.velocity = new Vector2(movementSpeed, rb.velocity.y);

    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        movementSpeed *= -1;
        dirX *= -1;
        mustPatrol = true;
        steps = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D player = Physics2D.OverlapArea(vision1, vision2, playerLayer);
        if (player == true)
        {
            Attack();
        }
    }

    void Attack()
    {
        if (attacking == false) {
            movementSpeed *= 2;
            maxSteps = 100000;
        }
        attacking = true;
    }

    public void Blocked()
    {
        stunned = true;
        counter = 80;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(vision1, vision2);
    }
}
