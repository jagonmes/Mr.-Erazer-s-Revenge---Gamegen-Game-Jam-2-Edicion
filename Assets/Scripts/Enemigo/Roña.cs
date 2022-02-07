using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roña : MonoBehaviour
{
    public float startContador = 3;
    private float contador = 3;
    private float timer = 2;

    public float timerTime = 2;

    private bool enabled = false;

    public Rigidbody2D rb;
    public Transform projectile;

    void Update()
    {
        if (contador == 0)
        {
            enabled = false;
        }
        if (timer <= 0 && enabled)
        {
            Shoot();
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        timer = timerTime;
        if ((int)Mathf.Round(Random.Range(0f, 1f)) == 1)
        {
            Instantiate(projectile, rb.position, Quaternion.identity);
        }
        contador--;
    }

    public void Enable()
    {
        enabled = true;
        timer = timerTime;
        contador = startContador;
    }
}
