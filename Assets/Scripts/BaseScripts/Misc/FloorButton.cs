using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{
    
    protected SSPlayerController playerController;
    protected Animator animator;
    protected bool previousActivatorState;
    public bool hasActivator;
    public Animator[] laserBarrierAnimators;
    public GameObject activationSfx;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (playerController.isRollerBot || playerController.isPlayer || playerController.isGuardBot)
            {
                ActivateFloorButton();
                hasActivator = true;
            }
        }

        if(collision.tag == "PlayerAnchor")
        {
            ActivateFloorButton();
            hasActivator = true;
        }

        if(collision.tag == "Enemy")
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            if(enemyController.enemyType == EnemyController.EnemyType.GuardBot || enemyController.enemyType == EnemyController.EnemyType.RollerBot)
            {
                ActivateFloorButton();
                hasActivator = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (hasActivator) { return; }

        if (collision.tag == "Player")
        {
            if (playerController.isRollerBot || playerController.isPlayer || playerController.isGuardBot)
            {
                DeactivateFloorButton();
            }
        }

        if (collision.tag == "Enemy")
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            if (enemyController.enemyType == EnemyController.EnemyType.GuardBot || enemyController.enemyType == EnemyController.EnemyType.RollerBot)
            {
                DeactivateFloorButton();
            }
        }
    }

    public virtual void Start()
    {
        playerController = FindObjectOfType<SSPlayerController>();
        animator = GetComponent<Animator>();
        DeactivateFloorButton();
    }

    private void FixedUpdate()
    {
        hasActivator = false;
    }

    private void ActivateFloorButton()
    {
        for (int i = 0; i < laserBarrierAnimators.Length; i++)
        {
            laserBarrierAnimators[i].SetBool("barrierOn", false);
        }
        
        animator.SetBool("isActive", true);
    }

    private void DeactivateFloorButton()
    {
        for (int i = 0; i < laserBarrierAnimators.Length; i++)
        {
            laserBarrierAnimators[i].SetBool("barrierOn", true);
        }
        
        animator.SetBool("isActive", false);
    }

    public void PlayActivationSfx()
    {
        print("playing activation");
        Instantiate(activationSfx, transform.position, transform.rotation);
    }
}
