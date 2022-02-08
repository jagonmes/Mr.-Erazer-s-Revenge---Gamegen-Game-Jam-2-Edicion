using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{

    public float speed;
    private float timeBtwShots;
    public float startTimeBtwShots;
    [SerializeField] private PlayerMovement pb;
    private float dir0 = 1;

    public bool canShoot = true;

    public Animator animator;
    
    public AudioSource shotSound;
    public Transform puntero;
    public GameObject projectile;

    public LayerMask enemies;
    // Start is called before the first frame update
    void Update()
    {   if (pb.GetDirX() != 0)
        {
           dir0 = pb.GetDirX();
           puntero.transform.position = new Vector2(this.transform.position.x + dir0*2, this.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.L) && timeBtwShots <=0 && canShoot)
        {
            animator.SetTrigger("throw");
            Shot();
            timeBtwShots = startTimeBtwShots;
        }
        if(timeBtwShots > 0){
            timeBtwShots -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Shot()
    {
        shotSound.Play();
        Instantiate(projectile, puntero.position, Quaternion.identity );
    }

}
