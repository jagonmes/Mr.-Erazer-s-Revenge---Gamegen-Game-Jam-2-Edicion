using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float speed;
    public float retreatDistance;
    public float shotingDistance;
    public float detectionDistance;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public Transform player;
    public Transform puntero;
    public GameObject projectile;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
    }


    void Update()
    {
        Debug.Log("Update");
        if (timeBtwShots <= 0 && Vector2.Distance(transform.position, player.position) < shotingDistance)
        {
            Instantiate(projectile, puntero.position, Quaternion.identity);

            timeBtwShots = startTimeBtwShots;
            Debug.Log("Disparo");
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
