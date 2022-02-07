using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roña : MonoBehaviour
{ 
    private float startContador;
    private float contador;

    public Rigidbody2D rb;
    public Transform projectile;
    // Start is called before the first frame update
    void Start()
    {
        startContador = 1;
        contador = startContador;
    }

    // Update is called once per frame
    void Update()
    {
        if(contador <= 0 ){
          if(Random.Range(0f,3f) > 1){
            Instantiate(projectile, rb.position, Quaternion.identity);
            contador = startContador;
          }
        }else{
        contador -= Time.deltaTime;
        }
    }
}
