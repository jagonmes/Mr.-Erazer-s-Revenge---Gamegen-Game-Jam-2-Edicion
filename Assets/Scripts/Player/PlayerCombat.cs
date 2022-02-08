using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool canAttack;
    public bool canBlock;
    public bool blocking = false;

    public Vector2 defensePoint2;

    public Transform defensePoint1;
    public Transform attackPoint;
    public Rigidbody2D rb;
    public Animator animator;
    public AudioSource swordSound;

    [SerializeField] private PlayerMovement pb;
    [SerializeField] private Disparar disparar;

    public float attackRange = 1f;
    public float defenseRange = 0.5f;
    public LayerMask enemies;
    public LayerMask projectiles;
    public float dir0 = 1;

    public float counter = 0;

    // Update is called once per frame
    void Update()
    {
        if (pb.GetDirX() != 0)
        {
            dir0 = pb.GetDirX();
            attackPoint.transform.position = new Vector2(this.transform.position.x + dir0, this.transform.position.y);
            defensePoint1.transform.position = new Vector2 (this.transform.position.x + dir0*0.5f, this.transform.position.y-1.2f);
            defensePoint2 = new Vector2(this.transform.position.x + 0.7f*dir0, this.transform.position.y+0.8f);
        }
        if (counter <= 0)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (canAttack)
                {
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("AttackLvL2"))
                    {
                        Attack();
                    }
                }
                
            }
        }
        else
        {
            counter--;
        }

        if (Input.GetKey(KeyCode.K))
        {
            if (canBlock)
            {
                Block();
                blocking = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            if (blocking) 
            {
                Debug.Log("entra");
                pb.canMove = true;
                canAttack = true;
                animator.SetBool("blocking", false);
                if (disparar != null)
                {
                    disparar.canShoot = true;
                }
            }
        }
    }

    void Attack()
    {

        animator.SetBool("attacking", true);
        
        swordSound.Play();
        
        counter = 60;
        Collider2D [] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemies);
        Debug.Log("ATTAAAAQUEEE");

        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<LapizEmbiste>() != null)
            {
                enemy.GetComponent<LapizEmbiste>().movementSpeed = 0;
            }
            else if (enemy.GetComponent<Enemy1>() != null)
            {
                enemy.GetComponent<Enemy1>().movementSpeed = 0;
            }
            Destroy(enemy.gameObject);
        }
    }

    void Block()
    {

        animator.SetBool("blocking", true);
        canAttack = false;
        pb.canMove = false;
        if (disparar != null)
        {
            disparar.canShoot = false;
        }

        rb.velocity = new Vector2(0, rb.velocity.y);

        Collider2D[] blockedEnemies = Physics2D.OverlapAreaAll(defensePoint1.position, defensePoint2, enemies);
        Collider2D[] blockedProjectiles = Physics2D.OverlapAreaAll(defensePoint1.position, defensePoint2, projectiles);
        Debug.Log("Defensa");
        foreach(Collider2D enemy in blockedEnemies)
        {
            if (enemy.GetComponent<LapizEmbiste>() != null)
            {
                enemy.GetComponent<LapizEmbiste>().Blocked();
            }
            else 
            {
                enemy.GetComponent<Enemy1>().Blocked();
            }

        }
        foreach (Collider2D projectile in blockedProjectiles)
        {
            Destroy(projectile.gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawLine(defensePoint1.position, defensePoint2);
    }

    public void StopAttack()
    {
        animator.SetBool("attacking", false);
    }
}
