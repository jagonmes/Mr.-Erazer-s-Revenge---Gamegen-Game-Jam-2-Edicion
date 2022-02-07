using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathline : MonoBehaviour
{

    public Collider2D other;
    private Transform player;

    private PlayerLife pl;
    private PlayerLife Sc;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Sc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
         //Enemies = GameObject.FindGameObjectWithTag("Enemy").transform;
    }
      void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Enemy")){
            Destroy(other.gameObject);

        } 
        if(other.CompareTag("Player")){
             Sc.Die();
            
        }  
        }
    }

