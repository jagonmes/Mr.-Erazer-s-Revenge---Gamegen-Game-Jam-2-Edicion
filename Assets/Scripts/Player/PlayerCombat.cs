using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Transform attackPoint;
    //public Transform attackPoint2;
    public Animator animator;

    [SerializeField] private PlayerMovement pb;

    public float attackRange = 1f;
    public LayerMask enemies;
    public LayerMask projectiles;
    public float dir0 = 1;

    // Update is called once per frame
    void Update()
    {
        if (pb.GetDirX() != 0)
        {
            dir0 = pb.GetDirX();
            attackPoint.transform.position = new Vector2(this.transform.position.x + dir0, this.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
        if (Input.GetKey(KeyCode.K))
        {
            Block();
        }
    }

    void Attack()
    {
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
}
