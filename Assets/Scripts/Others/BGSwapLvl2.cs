using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSwapLvl2 : MonoBehaviour
{
    public Transform player;

    public GameObject BG1;
    public GameObject BG2;
    public GameObject BG3;


    // Update is called once per frame

    void Start()
    {
        BG1.gameObject.SetActive(true);
        BG2.gameObject.SetActive(false);
        BG3.gameObject.SetActive(false);
    }

    void Update()
    {
        if (player.position.y < -18 && player.position.y > -60)
        {
            BG1.gameObject.SetActive(false);
            BG2.gameObject.SetActive(true);
            
        }
        else if (player.position.y < -60)
        {
            
            BG2.gameObject.SetActive(false);
            BG3.gameObject.SetActive(true);
            
        }

    }



}
