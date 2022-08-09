using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPossessor : MonoBehaviour
{
    private SSPlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<SSPlayerController>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            if(enemyController.enemyType != EnemyController.EnemyType.Bat && enemyController.enemyType != EnemyController.EnemyType.Spider)
            {
                playerController.possessableEnemies.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            if (enemyController.enemyType != EnemyController.EnemyType.Bat && enemyController.enemyType != EnemyController.EnemyType.Spider)
            {
                playerController.possessableEnemies.Remove(collision.gameObject);
            }
        }
    }
    
}
