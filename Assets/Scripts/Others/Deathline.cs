using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathline : MonoBehaviour
{

    public Collider2D other;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
         //Enemies = GameObject.FindGameObjectWithTag("Enemy").transform;
    }
      void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            Destroy(player);
        }
    }
}
