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

   [SerializeField] private Disparar disparar;

    private Transform player;



   void Start(){
       activoContador = contador;
       rb = GetComponent<Rigidbody2D>();
       player = GameObject.FindGameObjectWithTag("Player").transform;
       moveDirection = new Vector2((transform.position.x - player.position.x), 0);
       moveDirection = moveDirection / moveDirection.magnitude;
       rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
       Destroy(gameObject,tiempoBala);
    }
   void Update(){
        if (activoContador <= 0){
         Retroceso();
       }else{
          activoContador--;
       } 
   }

   
   void Retroceso(){
        moveDirection = new Vector2((player.position.x - transform.position.x), (player.position.y - transform.position.y));
        moveDirection = moveDirection / moveDirection.magnitude;
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }
   
   void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Enemy")){
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
