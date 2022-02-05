using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapizEmbiste : MonoBehaviour
{
    public bool mustPatrol;

    public float movementSpeed = 5;
    public int maxSteps = 100;
    public int steps = 0;

    private Rigidbody2D rb;
    public Transform transform;
    public BoxCollider2D collider;

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
        if (mustPatrol)
        {
            Patrol();
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
        mustPatrol = true;
        steps = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }

    void Attack()
    {
        movementSpeed *= 3;
    }
}
