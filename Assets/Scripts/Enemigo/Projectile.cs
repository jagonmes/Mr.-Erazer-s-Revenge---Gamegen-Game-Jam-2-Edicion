using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float tiempoTotal;
    private Transform player;
    private Vector2 target;
    private float contador;
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
        contador = 1;
        target = new Vector2(player.position.x, player.position.y);

    }

    void Update(){

        transform.position = Vector2.MoveTowards(transform.position , player.position , speed * Time.deltaTime);
        


        if(contador > tiempoTotal){
            DestroyProjectile();
        }
    }

    void DestroyProjectile(){

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            DestroyProjectile();
        }
    }
}
