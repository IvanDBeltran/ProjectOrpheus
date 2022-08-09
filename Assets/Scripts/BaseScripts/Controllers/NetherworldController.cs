using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetherworldController : MonoBehaviour
{

    public bool isNetherworld;
    public GameObject[] forestWorldObjects;
    public GameObject[] netherWorldObjects;
    public EnemyController[] enemyControllers;

    void Start()
    {
        enemyControllers = FindObjectsOfType<EnemyController>();

        if (isNetherworld)
        {
            ActivateNetherWorld();
        }
        else
        {
            ActivateForestWorld();
        }
    }

    public void ActivateNetherWorld()
    {
        isNetherworld = true;
        ActivateNetherWorldObjects();
        DeactivateForestWorldObjects();
        ActivateNetherWorldEnemies();
        DeactivateForestWorldEnemies();
    }

    public void ActivateForestWorld()
    {
        isNetherworld = false;
        ActivateForestWorldObjects();
        DeactivateNetherWorldObjects();
        ActivateForestWorldEnemies();
        DeactivateNetherWorldEnemies();
    }

    public void ActivateNetherWorldEnemies()
    {
        for (int i = 0; i < enemyControllers.Length; i++)
        {
            if(!enemyControllers[i].isDead && enemyControllers[i].isNetherWorldEnemy)
            {
                enemyControllers[i].gameObject.SetActive(true);
            }
        }
    }

    public void DeactivateNetherWorldEnemies()
    {
        for (int i = 0; i < enemyControllers.Length; i++)
        {
            if (!enemyControllers[i].isDead && enemyControllers[i].isNetherWorldEnemy)
            {
                enemyControllers[i].gameObject.SetActive(false);
            }
        }
    }

    public void ActivateForestWorldEnemies()
    {
        for (int i = 0; i < enemyControllers.Length; i++)
        {
            if (!enemyControllers[i].isDead && !enemyControllers[i].isNetherWorldEnemy)
            {
                enemyControllers[i].gameObject.SetActive(true);
            }
        }
    }

    public void DeactivateForestWorldEnemies()
    {
        for (int i = 0; i < enemyControllers.Length; i++)
        {
            if (!enemyControllers[i].isDead && !enemyControllers[i].isNetherWorldEnemy)
            {
                enemyControllers[i].gameObject.SetActive(false);
            }
        }
    }

    public void ActivateNetherWorldObjects()
    {
        for (int i = 0; i < netherWorldObjects.Length; i++)
        {
            netherWorldObjects[i].SetActive(true);
        }
    }

    public void DeactivateNetherWorldObjects()
    {
        for (int i = 0; i < netherWorldObjects.Length; i++)
        {
            netherWorldObjects[i].SetActive(false);
        }
    }

    public void ActivateForestWorldObjects()
    {
        for (int i = 0; i < forestWorldObjects.Length; i++)
        {
            forestWorldObjects[i].SetActive(true);
        }
    }

    public void DeactivateForestWorldObjects()
    {
        for (int i = 0; i < forestWorldObjects.Length; i++)
        {
            forestWorldObjects[i].SetActive(false);
        }
    }
}
