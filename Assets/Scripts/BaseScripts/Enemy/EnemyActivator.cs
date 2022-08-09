using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    private SSPlayerController playerController;

    public GameObject[] enemiesToActivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!playerController.isSpirit)
            {
                ActivateAllEnemies();
                this.gameObject.SetActive(false);
            }
        }
    }

    void Start()
    {
        playerController = FindObjectOfType<SSPlayerController>();
        DeactivateAllEnemies();
    }

    public void ActivateAllEnemies()
    {
        for (int i = 0; i < enemiesToActivate.Length; i++)
        {
            enemiesToActivate[i].SetActive(true);
        }
    }

    public void DeactivateAllEnemies()
    {
        for (int i = 0; i < enemiesToActivate.Length; i++)
        {
            enemiesToActivate[i].SetActive(false);
        }
    }
}
