using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float sttoppingDistance;
    public float retreatDistance;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public Transform player;
    public GameObject projectile;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBtwShots = startTimeBtwShots;
    }

    
    void Update()
    {
        if(Vector2.Distance(transform.position, player.position)> sttoppingDistance){
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
           
        }else if(Vector2.Distance(transform.position, player.position) < sttoppingDistance && Vector2.Distance(transform.position, player.position)> retreatDistance ){
            transform.position = this.transform.position;
        }else if(Vector2.Distance(transform.position, player.position)< retreatDistance){
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        if(timeBtwShots <= 0){
            Instantiate(projectile, transform.position, Quaternion.identity );
            timeBtwShots = startTimeBtwShots;   
        }else{
            timeBtwShots -= Time.deltaTime;
        }
    }
}
