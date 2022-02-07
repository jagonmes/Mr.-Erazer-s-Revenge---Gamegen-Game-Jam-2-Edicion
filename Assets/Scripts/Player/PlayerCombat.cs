using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool canAttack;
    public bool canBlock;
    
    public Transform attackPoint;
    public Rigidbody2D rb;
    public Animator animator;
    public AudioSource swordSound;

    [SerializeField] private PlayerMovement pb;

    public float attackRange = 1f;
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
            }
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            canAttack = true;
            animator.SetBool("blocking", false);
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
            enemy.GetComponent<LapizEmbiste>().movementSpeed = 0;
            Destroy(enemy.gameObject);
        }
    }

    void Block()
    {

        animator.SetBool("blocking", true);
        canAttack = false;

        Collider2D[] blockedEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemies);
        Collider2D[] blockedProjectiles = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, projectiles);
        Debug.Log("Defensa");
        foreach(Collider2D enemy in blockedEnemies)
        {
            enemy.GetComponent<LapizEmbiste>().Blocked();

        }
        foreach (Collider2D projectile in blockedProjectiles)
        {
            Destroy(projectile.gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange );
    }

    public void StopAttack()
    {
        animator.SetBool("attacking", false);
    }
}
