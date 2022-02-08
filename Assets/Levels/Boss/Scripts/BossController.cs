using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    //Enemigos a crear
    public GameObject bossEnemy1;
    public GameObject bossEnemy2;
    public GameObject bossEnemy3;

    public Rigidbody2D rb;

    public Animator animator;

    public Roña[] virutas;

    //Temporizador para volver a realizar una acción
    public float delayTime = 5;
    private float timer = 5;

    //Enemigos que ha matado el jugador
    public int enemyCounter = 0;

    //Enemigos que han hecho spawn
    private int spawnCounter = 0;

    //Fase actual del Jefe
    private int phaseCounter = 0;

    //Trigger para terminar el nivel
    private bool end = false;

    //Animación del jefe
    private int animationCounter;

    //Numero total de animaciones
    private int animationNumber;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timer = timer - Time.deltaTime;
        if (timer <= 0 && !end)
        {
            CallBoss();
        }
    }

    private void CallBoss()
    {
        //Activar Final del Boss
        if (phaseCounter == 3 && spawnCounter == 9)
        {
            end = true;
        }

        if (!end)
        {

            //Cambio de fase
            if (phaseCounter <= 2 && spawnCounter == (phaseCounter + 1) * 3)
            {
                ChangePhase();
            }
            //Sacar enemigo
            else if (spawnCounter < (phaseCounter + 1) * 3)
            {
                DrawEnemy();
            }
            if (phaseCounter != 3)
            {
                timer = delayTime;
            }
            else
            {
                timer = 9;
            }
        }
        else
        {
            EndBoss();
        }

    }

    //Dibuja al enemigo (poner al final de la animación una llamada a SpawnEnemy())
    private void DrawEnemy()
    {
        animationCounter++;
        SetAnimation();
        SpawnEnemy();
    }

    //Crea al enemigo
    public void SpawnEnemy()
    {
        int enemy = (int)Mathf.Round(Random.Range(0.5f, 3.5f));
        if (enemy == 1)
        {
            Instantiate(bossEnemy1, new Vector2(transform.position.x,transform.position.y - 0.75f), Quaternion.identity);
        }
        else if (enemy == 2)
        {
            Instantiate(bossEnemy2, new Vector2(transform.position.x, transform.position.y - 0.75f), Quaternion.identity);
        }
        else 
        {
            Instantiate(bossEnemy3, new Vector2(transform.position.x, transform.position.y - 0.75f), Quaternion.identity);
        }
        spawnCounter++;
    }

    //Cambia de fase con la animación correspondiente
    private void ChangePhase()
    {
        phaseCounter++;
        SetAnimation();
        for (int n = 0; n < virutas.Length; n++)
        {
            virutas[n].GetComponent<Roña>().Enable();
        }
    }

    //Da pie a la animación final del Boss
    private void EndBoss()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = new Vector2(0, 4.0f);
        Invoke("EndLevel", 3f);
    }

    public void EndLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void SetAnimation()
    {
        animator.SetInteger("animCounter", animationCounter);
        animator.SetInteger("phaseCounter", phaseCounter);
    }
}
