using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBatController : EnemyFlyingController
{

    void OnEnable()
    {
        targetTransform = playerController.gameObject.transform;

        StartCoroutine(FindNewTargetPosition());
    }

    public override void Update()
    {
        CheckPlayerInRange();
        if (playerInRange)
        {
            SetTarget();
            MoveTowardsTargetPosition();
            SetFacingDirection();
        }
    }

    

    public override void SetTarget()
    {
        targetTransform = playerController.gameObject.transform;      
    }

    public override void SetFacingDirection()
    {
        if (targetTransform.position.x < transform.position.x)
        {
            isFacingRight = false;
            transform.eulerAngles = new Vector3(0, 180f, 0);
        }
        else
        {
            isFacingRight = true;
            transform.eulerAngles = new Vector3(0, 0f, 0);
        }
    }

    private void SetTargetPosition()
    {
        Vector3 nextTargetPosition = Vector3.zero;

        if (playerController.isSpirit)
        {
            nextTargetPosition = targetTransform.position;
        }

        targetPosition = nextTargetPosition;
    }

    private IEnumerator FindNewTargetPosition()
    {
        SetTargetPosition();

        yield return new WaitForSeconds(timeBetweenTargeting);

        StartCoroutine(FindNewTargetPosition());
    }


    public override void CheckPlayerInRange()
    {
        playerInRange = false;

        if (Vector2.Distance(transform.position, playerController.gameObject.transform.position) < shootingDistance)
        {
            playerInRange = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingDistance);
    }
}

