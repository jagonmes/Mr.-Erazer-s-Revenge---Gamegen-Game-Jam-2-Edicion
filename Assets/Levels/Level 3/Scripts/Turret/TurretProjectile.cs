using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    public float speed;
    public float tiempoBala;
    private PlayerLife Sc;

    public Rigidbody2D rb;

    private Vector2 moveDirection;

    private Transform player;
    private PlayerLife pl;


    void Start()
    {
        Sc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        moveDirection = new Vector2((0 - rb.position.x), 0).normalized;
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        Destroy(gameObject, tiempoBala);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Sc.Die();
            Destroy(gameObject);
        }
    }
}
