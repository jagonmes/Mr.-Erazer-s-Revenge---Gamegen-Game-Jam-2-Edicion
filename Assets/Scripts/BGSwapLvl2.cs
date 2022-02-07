using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSwapLvl2 : MonoBehaviour
{
    public Sprite BG1;
    public Sprite BG2;
    public Sprite BG3;

    public SpriteRenderer fondo;

    public Collider2D trigger1;
    public Collider2D trigger2;

    // Update is called once per frame
    private void start()
    {

        fondo.sprite = BG1;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(fondo.sprite == BG1)
        {
            fondo.sprite = BG2;
        }
        else
        {
            fondo.sprite = BG3;
        }
    }

}
