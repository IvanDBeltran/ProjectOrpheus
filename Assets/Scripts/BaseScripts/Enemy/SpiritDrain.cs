using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritDrain : MonoBehaviour
{

    private SSPlayerController playerController;
    public GameObject spiritDrainSfx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine(DrainSpirit());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StopAllCoroutines();
        }
    }

    void Start()
    {
        playerController = FindObjectOfType<SSPlayerController>();
    }

    IEnumerator DrainSpirit()
    {
        if (!playerController.spiritIsBeingDrained)
        {
            PlaySpiritDrainSfx();
        }

        playerController.DrainSpirit();
        playerController.UpdateSpiritHealth();
        
        
        if (playerController.currentSpiritHealth <= 0 && playerController.isSpirit && !playerController.isTransitioning)
        {
            playerController.StartTransitionToPlayer();
            yield break;
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(DrainSpirit());
    }

    public void PlaySpiritDrainSfx()
    {
        Instantiate(spiritDrainSfx, transform.position, transform.rotation);
    }
}
