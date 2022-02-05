using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Platform : MonoBehaviour
{
    private int currentWaypointIndex = 0;
    private bool start = false;
    private bool end = false;
    [SerializeField] private PlayerMovement pm;

    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 2.0f;

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && end)
        {
            collision.gameObject.transform.SetParent(null);
            pm.CanMoveToggle();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !start && !end)
        {
            collision.gameObject.transform.SetParent(transform);
            start = true;
            pm.CanMoveToggle();
        }
    }

    private void Update()
    {
        if(start)
        { 
            if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    start = false;
                    end = true;
                }
            }

            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        }
    }
}
