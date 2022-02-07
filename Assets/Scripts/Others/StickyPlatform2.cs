using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform2 : MonoBehaviour
{

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            collision.gameObject.transform.SetParent(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }
}
