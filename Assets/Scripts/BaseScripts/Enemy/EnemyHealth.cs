using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private EnemyController enemyController;
    private Rigidbody2D rigidbodyEnemy;
    private float originalGravityScale;
    public GameObject hurtSfx;
    public GameObject deathSfx;

    public override void Awake()
    {
        base.Awake();
        enemyController = GetComponent<EnemyController>();
        rigidbodyEnemy = GetComponent<Rigidbody2D>();
        originalGravityScale = rigidbodyEnemy.gravityScale;
    }

    public override void Damage(int damageAmount)
    {

        if (isInvunerable || enemyController.enemyType == EnemyController.EnemyType.RollerBot)
        {
            return;
        }

        
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Kill();
            return;
        }

        PlayHurtSfx();
        isInvunerable = true;
        animator.SetBool("isDamaged", true);
        StopCoroutine(FlickerImage());
        StartCoroutine(FlickerImage());
        Invoke("MakeEnemyVulnerable", invincibilityTime);
    }

    public override void Kill()
    {
        if (!enemyController.isDead)
        {
            PlayDeathSfx();
        }
        
        animator.SetBool("isDead", true);
        gameObject.layer = 12;
        rigidbodyEnemy.gravityScale = 1f;
        rigidbodyEnemy.bodyType = RigidbodyType2D.Dynamic;

        if (enemyController != null)
        {
            enemyController.ChangeStateToDead();
        }
    }

    public override void ResetHealth()
    {
        currentHealth = maxHealth;
        gameObject.layer = originalLayer;
        rigidbodyEnemy.gravityScale = originalGravityScale;
    }

    public void MakeEnemyVulnerable()
    {
        isInvunerable = false;
        spriteRenderer.color = normalColor;
    }

    public void PlayHurtSfx()
    {
        Instantiate(hurtSfx, transform.position, transform.rotation);
    }

    public void PlayDeathSfx()
    {
        Instantiate(deathSfx, transform.position, transform.rotation);
    }
}
