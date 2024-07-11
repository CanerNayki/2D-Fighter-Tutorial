using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayer;

    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int attackDamage = 40;


    public void DamageEnemy()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            FindObjectOfType<AudioManager>().Play("swordsound2");
            FindObjectOfType<AudioManager>().Play("enemyhurt");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }   

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    
    
}
