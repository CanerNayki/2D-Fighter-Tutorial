using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Transform enemyAttackPoint;
    public LayerMask playerLayers;

    [SerializeField] private float enemyAttackRange = 0.5f;
    [SerializeField] private int enemyAttackDamage = 40;


    public void DamagePlayer()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(enemyAttackPoint.position, enemyAttackRange, playerLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<CharacterHealth>().TakeDamage(enemyAttackDamage);
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (enemyAttackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(enemyAttackPoint.position, enemyAttackRange);
    }

}
