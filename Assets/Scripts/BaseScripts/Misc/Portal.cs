using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    private NetherworldController netherworldController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (netherworldController.isNetherworld)
            {
                netherworldController.ActivateForestWorld();
            }
            else
            {
                netherworldController.ActivateNetherWorld();
            }
        }
    }

    void Start()
    {
        netherworldController = FindObjectOfType<NetherworldController>();    
    }
}
