using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProRoña : MonoBehaviour
{
   public float speed;
   public float tiempoBala;
   private PlayerLife Sc;

   public Rigidbody2D rb;

   private Transform player;
   private PlayerLife pl;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Sc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
        rb.velocity = new Vector2(0, -speed);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(gameObject, tiempoBala);
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            Sc.Die();
            Destroy(gameObject);
        }
    }
}
