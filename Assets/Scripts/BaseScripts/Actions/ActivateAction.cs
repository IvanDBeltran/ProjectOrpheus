using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAction : MonoBehaviour
{

    private ActionController actionController;
    private SpriteRenderer spriteRenderer;
    private SSPlayerController playerController;

    public bool canBeActivated;

    public SingleAction actionToActivate;
    public bool showSprite;
    public bool isPlayerInputActivated;

    public bool isActivatedBySpirit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canBeActivated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canBeActivated = false;
        }
    }
    void Update()
    {
        if (isPlayerInputActivated && canBeActivated && Input.GetButtonDown("Fire3") && !GameManager.Instance.inventoryActive)
        {
            if (!isActivatedBySpirit)
            {
                if (playerController.isPlayer)
                {
                    ActivateNextAction();
                }
            }
            else
            {
                if (playerController.isSpirit)
                {
                    ActivateNextAction();
                }
            }
        }
        else if (!isPlayerInputActivated && canBeActivated)
        {
            if (!isActivatedBySpirit)
            {
                if (playerController.isPlayer)
                {
                    ActivateNextAction();
                    this.gameObject.SetActive(false);
                }
            }
            else
            {
                if (playerController.isSpirit)
                {
                    ActivateNextAction();
                    this.gameObject.SetActive(false);
                }
            }
        }
    }

    void Start()
    {
        actionController = FindObjectOfType<ActionController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = FindObjectOfType<SSPlayerController>();
        if (!showSprite && spriteRenderer != null){ spriteRenderer.enabled = false; }
         
    }

    public void ActivateNextAction()
    {
        actionController.DeactivateAllActions();
        actionToActivate.gameObject.SetActive(true);
    }
}
