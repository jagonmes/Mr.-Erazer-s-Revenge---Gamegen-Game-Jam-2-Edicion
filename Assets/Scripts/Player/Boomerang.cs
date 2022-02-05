using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
   public float speed;
   public float tiempoBala;
   
   public Rigidbody2D rb;

   public float contador;
   private float activoContador;
   public float tiempoVolver;

   private Vector2 moveDirection;
   private Vector2 returnMoveDirection;

   private Transform player;



   void Start(){
       activoContador = contador;
       rb = GetComponent<Rigidbody2D>();
       player = GameObject.FindGameObjectWithTag("Player").transform;
       moveDirection = new Vector2(speed, 0);
       rb.velocity = new Vector2(moveDirection.x*speed, moveDirection.y*speed);
       Destroy(gameObject,tiempoBala);
   }
   void Update(){
       if (activoContador == 0){
          Retroceso();
          activoContador = contador;
       }else{
          activoContador--;
       }
   }
   void Retroceso(){
       //returnMoveDirection = new Vector2((player.position.x - rb.position.x), (player.position.x - rb.position.x)).normalized;
       moveDirection = new Vector2(-speed*2,0 ).normalized;
       rb.velocity = new Vector2(returnMoveDirection.x*speed, returnMoveDirection.y*speed);
   }
   
   void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Enemy")){
            Destroy(gameObject);
        }
}
}
