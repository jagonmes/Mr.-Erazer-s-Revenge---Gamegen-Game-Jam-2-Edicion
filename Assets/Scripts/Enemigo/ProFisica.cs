using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProFisica: MonoBehaviour
{
   public float speed;
   public float tiempoBala;

   public Rigidbody2D rb;

   public float tiempo;
   private float contador;

   private Vector2 moveDirection;

   private Transform player;



   void Start(){
       contador = 0;
       player = GameObject.FindGameObjectWithTag("Player").transform;
       moveDirection = new Vector2((player.position.x - rb.position.x), (player.position.y - rb.position.y)).normalized;
       rb.velocity = new Vector2(moveDirection.x*speed, moveDirection.y*speed);
       Destroy(gameObject, tiempoBala);
   }
   void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            Destroy(gameObject);
        }
}
}