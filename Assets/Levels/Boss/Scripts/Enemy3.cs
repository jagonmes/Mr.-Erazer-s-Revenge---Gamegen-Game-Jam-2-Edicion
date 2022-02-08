using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public float speed;
    public float retreatDistance;
    public float shotingDistance;
    public float detectionDistance;

    public BossController bc;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private bool set = false;

    public float p;

    private Rigidbody2D rb;
    public Transform player;
    public Transform puntero;
    public Transform flipValidator1;
    public Transform flipValidator2;
    public GameObject projectile;
    public AudioSource disparo;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
        rb = GetComponent<Rigidbody2D>();
        transform.Rotate(0f, 180f, 0f);
    }


    void Update()
    {
        p = Vector2.Distance(transform.position, player.position);
        if (!set) 
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(0f , transform.position.y), speed * Time.deltaTime);
            if (rb.position.x < 0.5f)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                set = true;
            }
        }
        else 
        {
            if (Vector2.Distance(flipValidator1.position, player.position) < Vector2.Distance(flipValidator2.position, player.position))
            {
                if (transform.rotation.y == 180)
                {
                    transform.Rotate(0f, 0f, 0f);
                }
                else
                {
                    transform.Rotate(0f, 180f, 0f);
                }
            }
            if (Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                if((Vector2.Distance(transform.position, player.position) - retreatDistance) > 0.3f) 
                { 
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                }
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player.position) < detectionDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }

            if (timeBtwShots <= 0 && Vector2.Distance(transform.position, player.position) < shotingDistance)
            {
                disparo.Play();
                Instantiate(projectile, puntero.position, Quaternion.identity);

                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
       
    }

    void OnDestroy()
    {
        Debug.Log("Arquero Muerto");
        bc.enemyCounter++;
    }
}
