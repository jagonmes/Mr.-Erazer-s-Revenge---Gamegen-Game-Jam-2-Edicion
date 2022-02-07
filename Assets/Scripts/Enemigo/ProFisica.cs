using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProFisica: MonoBehaviour
{
   public float speed;
   public float tiempoBala;
   private PlayerLife Sc;

   public Rigidbody2D rb;

   private Vector2 moveDirection;

   private Transform player;
   private PlayerLife pl;


   void Start(){
       rb.transform.Rotate(0,180,0);
       Sc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
       rb = GetComponent<Rigidbody2D>();
       player = GameObject.FindGameObjectWithTag("Player").transform;
       moveDirection = new Vector2((player.position.x - rb.position.x), (player.position.y - rb.position.y)).normalized;
       rb.velocity = new Vector2(moveDirection.x*speed, moveDirection.y*speed);
       Destroy(gameObject, tiempoBala);
   }
   void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            Sc.Die();
            Destroy(gameObject);
        }
    }
}