using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private bool isRespawningEnemy;
    private EnemyController enemyController;
    private EnemyHealth enemyHealth;
    private Animator animator;
    private NetherworldController netherworldController;

    public GameObject enemyToRespawn;
    public float timeBetweenRespawns;
    public Animator lightAnimator;

    void Start()
    {
        enemyController = enemyToRespawn.GetComponent<EnemyController>();
        enemyHealth = enemyToRespawn.GetComponent<EnemyHealth>();
        netherworldController = FindObjectOfType<NetherworldController>();
        animator = GetComponent<Animator>();
        animator.SetBool("isOpen", false);
    }

    void Update()
    {
        if (enemyController.isDead && !isRespawningEnemy)
        {
            isRespawningEnemy = true;
            StartCoroutine(StartRespawn());
        }
    }

    private IEnumerator StartRespawn()
    {
        
        yield return new WaitForSeconds(timeBetweenRespawns);
        lightAnimator.SetBool("isOn", true);

        yield return new WaitForSeconds(.5f);

        animator.SetBool("isOpen", true);

        yield return new WaitForSeconds(.5f);

        RespawnEnemy();

        if(netherworldController != null)
        {
            if(netherworldController.isNetherworld && !enemyController.isNetherWorldEnemy)
            {
                enemyToRespawn.SetActive(false);
            }
        }
        yield return new WaitForSeconds(.5f);

        animator.SetBool("isOpen", false);
        lightAnimator.SetBool("isOn", false);
        isRespawningEnemy = false;
    }

    private void RespawnEnemy()
    {
        enemyController.PlaceAtOriginalPosition();
        enemyHealth.ResetHealth();
        enemyToRespawn.SetActive(true);
    }
}
