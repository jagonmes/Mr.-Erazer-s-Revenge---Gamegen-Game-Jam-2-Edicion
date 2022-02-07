using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    //Enemigos a crear
    public GameObject bossEnemy1;
    public GameObject bossEnemy2;
    public GameObject bossEnemy3;

    //Temporizador para volver a realizar una acción
    public float delayTime = 5;
    private float timer = 5;

    //Enemigos que ha matado el jugador
    public int enemyCounter = 0;

    //Enemigos que han hecho spawn
    private int spawnCounter = 0;

    //Fase actual del Jefe
    private int phaseCounter = 1;

    //Trigger para terminar el nivel
    private bool end = false;

    //Animación del jefe
    private int animationCounter;

    //Numero total de animaciones
    private int animationNumber;

    void Update()
    {
        timer = timer - Time.deltaTime;
        if (timer <= 0 && !end)
        {
            CallBoss();
        }
        else 
        {
            EndBoss();
        }
    }

    private void CallBoss() 
    {
        //Activar Final del Boss
        if (phaseCounter == 3 && enemyCounter == 9)
        {
            end = true;
        }
        
        //Sacar enemigo
        if (spawnCounter < phaseCounter * 3)  
        {
            DrawEnemy();
        }

        //Cambio de fase
        if (phaseCounter < 3 && spawnCounter == phaseCounter * 3 && spawnCounter == 9) 
        {
            ChangePhase();
        }
        timer = delayTime;
    }

    //Dibuja al enemigo (poner al final de la animación una llamada a SpawnEnemy())
    private void DrawEnemy()
    {
        SetAnimation();
        SpawnEnemy();
        animationCounter++;
    }

    //Crea al enemigo
    public void SpawnEnemy() 
    {
        int enemy = (int)Mathf.Round(Random.Range(1, 3));
        if (enemy == 1)
        {
            Instantiate(bossEnemy1, transform.position, Quaternion.identity);
        }
        else if (enemy == 2)
        {
            Instantiate(bossEnemy2, transform.position, Quaternion.identity);
        }
        else 
        {
            Instantiate(bossEnemy3, transform.position, Quaternion.identity);
        }
        spawnCounter++;
    }

    //Cambia de fase con la animación correspondiente
    private void ChangePhase()
    {
        phaseCounter++;
        SetAnimation();
        animationCounter++;
    }

    //Da pie a la animación final del Boss (poner al finalo de la animación una llamada a EndLevel())
    private void EndBoss() 
    {
        SetAnimation();
    }

    public void EndLevel() 
    {
        //Poner codigo de final de nivel (salto del Jefe al vacio, musiquita, y cambio de escena a los creditos)
    }

    private void SetAnimation() 
    {
        /*
        if (animationCounter < animationNumber) 
        {
            anim.SetInteger("state", animationCounter);
            if (end) 
            {
                animationCounter = animationNumber;
            }
        }
        */
    }
}
