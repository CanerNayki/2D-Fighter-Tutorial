using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public bool enemyAttack;

    public float enemyTimer;

    public Animator anim;
    void Start()
    {
        currentHealth = maxHealth;
        enemyTimer = 1f;
        anim = GetComponent<Animator>();

    }

    void EnemyAttackSpacing()
    {
        if (enemyAttack == false)
        {
            enemyTimer -= Time.deltaTime;
        }
        if (enemyTimer < 0) 
        {
            enemyTimer = 0f;
        }
        if(enemyTimer == 0f)
        {
            enemyAttack = true;
            enemyTimer = 1f;
        }
    }

    void CharacterDamage()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            enemyAttack = false;
        }
    }
    public void TakeDamage(int damage)
    {
        if(enemyAttack)
        {
            currentHealth -= 20;
            enemyAttack = false;
            anim.SetTrigger("isHurt");
        }
        healthBar.SetHealth(currentHealth);


        if (currentHealth <= 0)
        {
            currentHealth = 0;  
            Die();
        }
    }

    void Die()
    {
        anim.SetBool("isDead", true);

        GetComponent<CharacterMovement>().enabled = false;

        Destroy(gameObject, 2f);
    }


    void Update()
    {
        EnemyAttackSpacing();
        CharacterDamage();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(20);
        }
    }
}
